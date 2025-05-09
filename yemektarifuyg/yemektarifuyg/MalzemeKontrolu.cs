using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace yemektarifuyg
{
    internal class MalzemeKontrolu
    {
        private string connectionString;

        public MalzemeKontrolu(string connString)
        {
            connectionString = connString;
        }

        public void KontrolEt(List<string> tarifAdlari)
        {
            VeriTabaniBaglantisi.BaglantiKontrolu();

            foreach (var tarifAdi in tarifAdlari)
            {
                int tarifID = GetTarifID(tarifAdi);
                if (tarifID != -1)
                {
                    List<int> eksikMalzemeIDler = new List<int>();
                    decimal toplamMaliyet = 0;

                    // Eksik malzeme kontrolünü yapıyorum
                    bool eksikMalzemeVarMi = EksikMalzemeVarMi(tarifID, eksikMalzemeIDler, ref toplamMaliyet);

                    // Eksik malzeme durumu ve maliyetini güncelliyorum.
                    EksikMalzemeDurumunuGüncelle(tarifID, eksikMalzemeVarMi, toplamMaliyet);
                }
            }

            VeriTabaniBaglantisi.baglanti.Close();
        }

        private int GetTarifID(string tarifAdi)
        {
            string query = "SELECT TarifID FROM Tarifler WHERE TarifAdi = @TarifAdi";
            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                object result = cmd.ExecuteScalar();
                int tarifID = result != null ? Convert.ToInt32(result) : -1;
                Console.WriteLine($"Tarif '{tarifAdi}' için TarifID: {tarifID}");
                return tarifID;
            }
        }

        private bool EksikMalzemeVarMi(int tarifID, List<int> eksikMalzemeIDler, ref decimal toplamMaliyet) 
        {
            string query = @"
            SELECT m.ToplamMiktar, tm.MalzemeMiktar, tm.MalzemeID, m.BirimFiyat 
            FROM TarifMalzemeIliskisi tm
            INNER JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID
            WHERE tm.TarifID = @TarifID";

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                cmd.Parameters.AddWithValue("@TarifID", tarifID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    bool eksikVarMi = false;

                    while (reader.Read())
                    {
                        decimal toplamMiktar = Convert.ToDecimal(reader["ToplamMiktar"]);
                        decimal malzemeMiktar = Convert.ToDecimal(reader["MalzemeMiktar"]); 
                        int malzemeID = Convert.ToInt32(reader["MalzemeID"]);
                        decimal birimFiyat = Convert.ToDecimal(reader["BirimFiyat"]);

                    
                        if (toplamMiktar < malzemeMiktar)
                        {
                            eksikMalzemeIDler.Add(malzemeID);
                            decimal eksikMiktar = malzemeMiktar - toplamMiktar;
                            toplamMaliyet += eksikMiktar * birimFiyat; 
                            eksikVarMi = true;
                            Console.WriteLine($"Eksik malzeme bulundu. MalzemeID: {malzemeID}, Gerekli: {malzemeMiktar}, Mevcut: {toplamMiktar}");
                        }
                    }

                    Console.WriteLine(eksikVarMi ? "Eksik malzeme var." : "Eksik malzeme yok.");
                    return eksikVarMi;
                }
            }
        }

        private void EksikMalzemeDurumunuGüncelle(int tarifID, bool eksikMalzemeVarMi, decimal eksikMalzemeMaliyeti) 
        {
            string query = @"
            UPDATE Tarifler 
            SET EksikMalzemeVarMi = @EksikMalzemeVarMi, 
                EksikMalzemeMaliyeti = @EksikMalzemeMaliyeti
            WHERE TarifID = @TarifID";

            using (SqlCommand cmd = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
            {
                // Eksik malzeme yoksa maliyeti 0 yap
                decimal maliyet = eksikMalzemeVarMi ? eksikMalzemeMaliyeti : 0;
                cmd.Parameters.AddWithValue("@EksikMalzemeVarMi", eksikMalzemeVarMi ? 1 : 0);
                cmd.Parameters.AddWithValue("@EksikMalzemeMaliyeti", maliyet);
                cmd.Parameters.AddWithValue("@TarifID", tarifID);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"TarifID {tarifID}: Eksik malzeme durumu güncellendi. Maliyet: {maliyet}");
            }
        }
    }
}
