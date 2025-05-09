using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class ToplamMaliyetHesaplama
{


    private string connectionString;

    public ToplamMaliyetHesaplama(string connString)
    {
        connectionString = connString;
    }

    // Tariflerin toplam maliyetini hesaplıyorum  ve Tarifler tablosundaki ToplamMaliyet kolonunu güncelliyorum.
    public void TarifToplamMaliyetHesapla(List<string> tarifAdlari)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (var tarifAdi in tarifAdlari)
            {
                // Tarifin ID'sini bulmak için sorgu
                string tarifIdQuery = "SELECT TarifID FROM Tarifler WHERE TarifAdi = @TarifAdi";
                SqlCommand tarifIdCmd = new SqlCommand(tarifIdQuery, connection);
                tarifIdCmd.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                int tarifId = Convert.ToInt32(tarifIdCmd.ExecuteScalar());

               
                string tarifMaliyetQuery = @"
                        SELECT t.MalzemeID, t.MalzemeMiktar, m.BirimFiyat 
                        FROM TarifMalzemeIliskisi t
                        JOIN Malzemeler m ON t.MalzemeID = m.MalzemeID
                        WHERE t.TarifID = @TarifID";

                SqlCommand cmd = new SqlCommand(tarifMaliyetQuery, connection);
                cmd.Parameters.AddWithValue("@TarifID", tarifId);
                SqlDataReader reader = cmd.ExecuteReader();

                double toplamMaliyet = 0;

                while (reader.Read())
                {
                    double malzemeMiktar = Convert.ToDouble(reader["MalzemeMiktar"]);
                    double birimFiyat = Convert.ToDouble(reader["BirimFiyat"]);

                    // Maliyet hesaplıyorum.
                    double maliyet = (malzemeMiktar * birimFiyat);
                    toplamMaliyet += maliyet;
                }

                reader.Close();

              
                UpdateToplamMaliyet(tarifId, toplamMaliyet, connection);
            }
        }
    }

    // Toplam maliyeti Tarifler tablosundaki ToplamMaliyet kolonunda güncelliyorum.
    private void UpdateToplamMaliyet(int tarifID, double toplamMaliyet, SqlConnection connection)
    {
        string updateQuery = "UPDATE Tarifler SET ToplamMaliyet = @ToplamMaliyet WHERE TarifID = @TarifID";
        using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
        {
            cmd.Parameters.AddWithValue("@ToplamMaliyet", toplamMaliyet);
            cmd.Parameters.AddWithValue("@TarifID", tarifID);
            cmd.ExecuteNonQuery();
        }
    }



}
