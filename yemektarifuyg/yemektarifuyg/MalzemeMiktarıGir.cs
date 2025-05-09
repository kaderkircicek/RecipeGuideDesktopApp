using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace yemektarifuyg
{
    public partial class MalzemeMiktarıGir : Form
    {
        private int tarifID;
        private List<int> malzemeIDListesi;
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True;";
        public MalzemeMiktarıGir(int tarifID, List<int> malzemeIDListesi)
        {
            InitializeComponent();
            this.tarifID = tarifID;
            this.malzemeIDListesi = malzemeIDListesi;
            MalzemeleriListele(); 
        }



        private void MalzemeleriListele()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    DataTable malzemeTable = new DataTable();

                    // MalzemeBirim sütununu da sorguya ekledim.
                    string query = "SELECT MalzemeID, MalzemeAdi, MalzemeBirim FROM Malzemeler WHERE MalzemeID IN (" + string.Join(",", malzemeIDListesi) + ")";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(malzemeTable);
                        }
                    }


                    dataGridViewMalzemeler.DataSource = malzemeTable;


                    dataGridViewMalzemeler.Columns.Add("KullanilacakMiktar", "Tarifte Kullanılacak Miktar");

                    foreach (DataGridViewRow row in dataGridViewMalzemeler.Rows)
                    {
                        row.Cells["KullanilacakMiktar"].Value = 0;
                    }


                    dataGridViewMalzemeler.Columns["MalzemeBirim"].HeaderText = "Birim";


                    dataGridViewMalzemeler.Columns["MalzemeID"].Visible = false;

                    dataGridViewMalzemeler.Columns["MalzemeAdi"].Width = 150; // Malzeme Adı sütunu genişliği
                    dataGridViewMalzemeler.Columns["MalzemeBirim"].Width = 100; // Birim sütunu genişliği
                    dataGridViewMalzemeler.Columns["KullanilacakMiktar"].Width = 150; // Kullanılacak Miktar sütunu genişliği
                
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Malzemeleri listelerken hata: " + ex.Message);
            }
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Her malzeme için miktar bilgilerini alıp ve TarifMalzemeIliskisi tablosuna ekliyorum.
                    foreach (DataGridViewRow row in dataGridViewMalzemeler.Rows)
                    {
                        int malzemeID = Convert.ToInt32(row.Cells["MalzemeID"].Value);
                        decimal malzemeMiktar = Convert.ToDecimal(row.Cells["KullanilacakMiktar"].Value);

                        if (!MalzemeMevcutMu(connection, malzemeID))
                        {
                          
                            continue;
                        }

                        // Önce güncelleme dene, eğer yoksa ekle
                        string updateQuery = "UPDATE TarifMalzemeIliskisi SET MalzemeMiktar = @MalzemeMiktar " +
                                             "WHERE TarifID = @TarifID AND MalzemeID = @MalzemeID";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@TarifID", tarifID);
                            updateCommand.Parameters.AddWithValue("@MalzemeID", malzemeID);
                            updateCommand.Parameters.AddWithValue("@MalzemeMiktar", malzemeMiktar);

                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            // Eğer güncelleme işlemi başarısız olduysa, yeni kayıt ekle
                            if (rowsAffected == 0)
                            {
                                string insertQuery = "INSERT INTO TarifMalzemeIliskisi (TarifID, MalzemeID, MalzemeMiktar) " +
                                                     "VALUES (@TarifID, @MalzemeID, @MalzemeMiktar)";

                                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@TarifID", tarifID);
                                    insertCommand.Parameters.AddWithValue("@MalzemeID", malzemeID);
                                    insertCommand.Parameters.AddWithValue("@MalzemeMiktar", malzemeMiktar);

                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    MessageBox.Show("Malzeme miktarları başarıyla kaydedildi.");
                    this.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }


        private bool MalzemeMevcutMu(SqlConnection connection, int malzemeID)
        {
            string checkQuery = "SELECT COUNT(1) FROM Malzemeler WHERE MalzemeID = @MalzemeID";
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@MalzemeID", malzemeID);
                return (int)checkCommand.ExecuteScalar() > 0; 
            }
        }




    }
}
