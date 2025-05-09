using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace yemektarifuyg
{
    public partial class MalzemeGuncellemeForm : Form
    {
        private int TarifID;
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";

        public MalzemeGuncellemeForm(int tarifID)
        {
            InitializeComponent();
            this.TarifID = tarifID;

            // Malzemeleri form açıldığında getir
            MalzemeleriTabloyaGetir();
        }

        private void MalzemeleriTabloyaGetir()
        {
            malzemeDataGridView.DataSource = null;
            malzemeDataGridView.Rows.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT M.MalzemeAdi, TMI.MalzemeMiktar, M.MalzemeBirim, M.BirimFiyat, M.ToplamMiktar
                                     FROM TarifMalzemeIliskisi TMI
                                     JOIN Malzemeler M ON TMI.MalzemeID = M.MalzemeID
                                     WHERE TMI.TarifID = @TarifID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TarifID", TarifID);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable malzemeTablosu = new DataTable();
                        adapter.Fill(malzemeTablosu);

                       
                        DataTable uniqueMalzemeTablosu = new DataTable();
                        uniqueMalzemeTablosu.Columns.Add("MalzemeAdi", typeof(string));
                        uniqueMalzemeTablosu.Columns.Add("MalzemeMiktar", typeof(float));
                        uniqueMalzemeTablosu.Columns.Add("MalzemeBirim", typeof(string));
                        uniqueMalzemeTablosu.Columns.Add("BirimFiyat", typeof(decimal));
                        uniqueMalzemeTablosu.Columns.Add("ToplamMiktar", typeof(float));

                       
                        foreach (DataRow row in malzemeTablosu.Rows)
                        {
                            string malzemeAdi = row["MalzemeAdi"].ToString();
                            bool exists = false;

                            foreach (DataRow uniqueRow in uniqueMalzemeTablosu.Rows)
                            {
                                if (uniqueRow["MalzemeAdi"].ToString() == malzemeAdi)
                                {
                                    exists = true;
                                    break;
                                }
                            }

                            if (!exists)
                            {
                                uniqueMalzemeTablosu.Rows.Add(row["MalzemeAdi"], row["MalzemeMiktar"], row["MalzemeBirim"], row["BirimFiyat"], row["ToplamMiktar"]);
                            }
                        }

                      
                        malzemeDataGridView.DataSource = uniqueMalzemeTablosu;
                        malzemeDataGridView.Columns["MalzemeAdi"].HeaderText = "Malzeme Adı";
                        malzemeDataGridView.Columns["MalzemeMiktar"].HeaderText = "Kullanılacak Miktar";
                        malzemeDataGridView.Columns["MalzemeBirim"].HeaderText = "Birim";
                        malzemeDataGridView.Columns["BirimFiyat"].HeaderText = "Birim Fiyatı";
                        malzemeDataGridView.Columns["ToplamMiktar"].HeaderText = "Depodaki Miktar";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void kaydetButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Güncellemeleri DataGridView'den veritabanına kaydet
                    foreach (DataGridViewRow row in malzemeDataGridView.Rows)
                    {
                        if (row.IsNewRow) continue; 

                        string malzemeAdi = row.Cells["MalzemeAdi"].Value.ToString();
                        float malzemeMiktar = float.Parse(row.Cells["MalzemeMiktar"].Value.ToString());
                        string malzemeBirim = row.Cells["MalzemeBirim"].Value.ToString();
                        decimal birimFiyat = decimal.Parse(row.Cells["BirimFiyat"].Value.ToString());
                        float toplamMiktar = float.Parse(row.Cells["ToplamMiktar"].Value.ToString()); // Depo miktarı

                        // MalzemeID'yi bulmak için MalzemeAdı üzerinden sorgu yapıyorum.
                        string malzemeIDQuery = "SELECT MalzemeID FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi";
                        using (SqlCommand malzemeIDCommand = new SqlCommand(malzemeIDQuery, connection))
                        {
                            malzemeIDCommand.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
                            int malzemeID = (int)malzemeIDCommand.ExecuteScalar();

                            // TarifMalzemeIliskisi ve Malzemeler tablolarını güncelliyorum.
                            string updateQuery = @"UPDATE TarifMalzemeIliskisi 
                                                   SET MalzemeMiktar = @MalzemeMiktar
                                                   WHERE TarifID = @TarifID AND MalzemeID = @MalzemeID;

                                                   UPDATE Malzemeler 
                                                   SET MalzemeBirim = @MalzemeBirim, BirimFiyat = @BirimFiyat, ToplamMiktar = @ToplamMiktar
                                                   WHERE MalzemeID = @MalzemeID;";

                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@MalzemeMiktar", malzemeMiktar);
                                updateCommand.Parameters.AddWithValue("@MalzemeBirim", malzemeBirim);
                                updateCommand.Parameters.AddWithValue("@BirimFiyat", birimFiyat);
                                updateCommand.Parameters.AddWithValue("@ToplamMiktar", toplamMiktar); // Depo miktarını güncelle
                                updateCommand.Parameters.AddWithValue("@TarifID", TarifID);
                                updateCommand.Parameters.AddWithValue("@MalzemeID", malzemeID);

                                updateCommand.ExecuteNonQuery(); 
                            }
                        }
                    }

                    MessageBox.Show("Malzeme ve depo güncellemeleri başarıyla kaydedildi.");
                    this.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
