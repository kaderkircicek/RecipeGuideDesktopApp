using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yemektarifuyg
{
    public partial class YeniMalzemeEklemeForm : Form
    {
        private int tarifID;
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True;";
        public YeniMalzemeEklemeForm(int tarifID)
        {
            InitializeComponent();
            this.tarifID = tarifID;
        }

        public event Action MalzemeEklendi;

        private void btnKaydet_Click(object sender, EventArgs e)
        {


            string malzemeAdi = txtMalzemeAdi.Text;
            string toplamMiktar = txtToplamMiktar.Text;
            string malzemeBirim = comboBoxMalzemeBirim.SelectedItem?.ToString();
            decimal birimFiyat;
            decimal malzemeMiktar;

            // Giriş doğrulaması
            if (string.IsNullOrEmpty(malzemeAdi))
            {
                MessageBox.Show("Lütfen malzeme adını giriniz.");
                return;
            }

            if (!decimal.TryParse(toplamMiktar, out decimal toplamMiktarDecimal))
            {
                MessageBox.Show("Depodaki  miktar geçerli bir sayı olmalıdır.");
                return;
            }

            if (string.IsNullOrEmpty(malzemeBirim))
            {
                MessageBox.Show("Lütfen malzeme birimini seçiniz.");
                return; 
            }

            if (!decimal.TryParse(txtBirimFiyat.Text, out birimFiyat))
            {
                MessageBox.Show("Birim fiyatı geçerli bir sayı olmalıdır.");
                return;
            }

            if (!decimal.TryParse(txtMalzemeMiktar.Text, out malzemeMiktar)) 
            {
                MessageBox.Show("Tarifte kullanılacak malzeme miktarı geçerli bir sayı olmalıdır.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                   
                    string queryMalzemeKontrol = "SELECT COUNT(*) FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi";
                    using (SqlCommand commandKontrol = new SqlCommand(queryMalzemeKontrol, connection))
                    {
                        commandKontrol.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
                        int count = (int)commandKontrol.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Bu malzeme adı zaten mevcut, yeni bir isim girin.");
                            return; 
                        }
                    }

                    
                    string queryTarifKontrol = "SELECT COUNT(*) FROM Tarifler WHERE TarifID = @TarifID";
                    using (SqlCommand command = new SqlCommand(queryTarifKontrol, connection))
                    {
                        command.Parameters.AddWithValue("@TarifID", tarifID);
                        int count = (int)command.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show(" Malzemenin kullanılacağı tarif bulunamadı. Tarif ekleme işlemini doğru yaptığınızdan emin olun.");
                            return;
                        }
                    }

                    // 1. Adım: Malzeme bilgilerini Malzemeler tablosuna kaydet
                    string queryMalzemeEkle = "INSERT INTO Malzemeler (MalzemeAdi, ToplamMiktar, MalzemeBirim, BirimFiyat) " +
                                              "VALUES (@MalzemeAdi, @ToplamMiktar, @MalzemeBirim, @BirimFiyat);";

                    using (SqlCommand command = new SqlCommand(queryMalzemeEkle, connection))
                    {
                        command.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
                        command.Parameters.AddWithValue("@ToplamMiktar", toplamMiktar);
                        command.Parameters.AddWithValue("@MalzemeBirim", malzemeBirim);
                        command.Parameters.AddWithValue("@BirimFiyat", birimFiyat);

                        command.ExecuteNonQuery();
                    }

                    // 2. Adım: Malzeme adı ile MalzemeID'yi bul
                    string queryMalzemeIDBul = "SELECT MalzemeID FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi";
                    int yeniMalzemeID;
                    using (SqlCommand commandMalzemeIDBul = new SqlCommand(queryMalzemeIDBul, connection))
                    {
                        commandMalzemeIDBul.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
                        yeniMalzemeID = Convert.ToInt32(commandMalzemeIDBul.ExecuteScalar());
                    }

                    // 3. Adım: TarifMalzemeIlişkisi tablosuna ekle
                    string queryTarifMalzemeEkle = "INSERT INTO TarifMalzemeIliskisi (TarifID, MalzemeID, MalzemeMiktar) " +
                                                   "VALUES (@TarifID, @MalzemeID, @MalzemeMiktar)";

                    using (SqlCommand command2 = new SqlCommand(queryTarifMalzemeEkle, connection))
                    {
                        command2.Parameters.AddWithValue("@TarifID", tarifID);
                        command2.Parameters.AddWithValue("@MalzemeID", yeniMalzemeID);
                        command2.Parameters.AddWithValue("@MalzemeMiktar", malzemeMiktar);

                        command2.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Malzeme başarıyla kaydedildi.");
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
