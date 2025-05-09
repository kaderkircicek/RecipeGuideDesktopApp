using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace yemektarifuyg
{
    internal class EksikMaliyetHesaplama
    {
        private string connectionString;

        public EksikMaliyetHesaplama(string connString)
        {
            connectionString = connString;
        }

        // Eksik malzeme maliyetini hesaplıyorum.
        public double EksikMalzemeMaliyetHesapla(List<int> malzemeIDler, int tarifID)
        {
            double toplamMaliyet = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var malzemeID in malzemeIDler)
                {
                    // Eksik malzemenin birim fiyatını ve eksik miktarını sorgula
                    string malzemeQuery = @"
                SELECT m.BirimFiyat, t.MalzemeMiktar, m.ToplamMiktar 
                FROM Malzemeler m
                JOIN TarifMalzemeIliskisi t ON m.MalzemeID = t.MalzemeID
                WHERE m.MalzemeID = @MalzemeID";

                    using (SqlCommand cmd = new SqlCommand(malzemeQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@MalzemeID", malzemeID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                double birimFiyat = Convert.ToDouble(reader["BirimFiyat"]);
                                double malzemeMiktar = Convert.ToDouble(reader["MalzemeMiktar"]);
                                double toplamMiktar = Convert.ToDouble(reader["ToplamMiktar"]);

                                // Eksik miktarı hesapla
                                double eksikMiktar = malzemeMiktar - toplamMiktar;
                                if (eksikMiktar > 0)
                                {
                                    // Eksik miktar için maliyeti hesaplıyorum ve toplam maliyete ekliyorum.
                                    double maliyet = eksikMiktar * birimFiyat;
                                    toplamMaliyet += maliyet;
                                }
                            }
                        }
                    }
                }

               
                UpdateEksikMalzemeMaliyeti(tarifID, toplamMaliyet);
            }

            return toplamMaliyet;
        }

        // Eksik malzeme maliyetini Tarifler tablosundaki EksikMalzemeMaliyeti sütununda güncelliyorum.
        private void UpdateEksikMalzemeMaliyeti(int tarifID, double toplamMaliyet)
        {
            string query = "UPDATE Tarifler SET EksikMalzemeMaliyeti = @EksikMalzemeMaliyeti WHERE TarifID = @TarifID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EksikMalzemeMaliyeti", toplamMaliyet);
                    cmd.Parameters.AddWithValue("@TarifID", tarifID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
