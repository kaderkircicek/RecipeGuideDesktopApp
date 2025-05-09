using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace yemektarifuyg
{
    public partial class AnaSayfaMalzemeEkleme : Form
    {
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True;";
        private AnaSayfa _form1;

        public AnaSayfaMalzemeEkleme(AnaSayfa form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string malzemeAdi = txtMalzemeAdi.Text;
            string toplamMiktar = txtToplamMiktar.Text;
            string malzemeBirim = comboBoxMalzemeBirim.SelectedItem?.ToString();
            decimal birimFiyat;

            // Eksik ya da yanlış girilmiş textboxları kontrol ediyorum.
            if (string.IsNullOrEmpty(malzemeAdi))
            {
                MessageBox.Show("Lütfen malzeme adını doldurunuz.");
                return;
            }

            if (string.IsNullOrEmpty(toplamMiktar))
            {
                MessageBox.Show("Lütfen depodaki miktarı doldurunuz.");
                return;
            }

            if (malzemeBirim == null)
            {
                MessageBox.Show("Lütfen malzeme birimini seçiniz.");
                return;
            }


            if (!decimal.TryParse(txtBirimFiyat.Text, out birimFiyat))
            {
                MessageBox.Show("Lütfen geçerli bir birim fiyat giriniz.");
                return;
            }
            if (!decimal.TryParse(toplamMiktar, out decimal toplamMiktarDecimal))
            {
                MessageBox.Show("Lütfen geçerli bir depo miktarı giriniz.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // 1. Adım: Malzeme adının varlığını kontrol ediyorum.
                    string queryMalzemeKontrol = "SELECT COUNT(*) FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi";
                    using (SqlCommand commandKontrol = new SqlCommand(queryMalzemeKontrol, connection))
                    {
                        commandKontrol.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
                        int count = (int)commandKontrol.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Bu malzeme adı zaten mevcut, yeni bir isim girin.");
                            return; // Hata durumunda işlemi sonlandır
                        }
                    }

                    // 2. Adım: Malzeme bilgilerini Malzemeler tablosuna kaydediyorum.
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
                }

                MessageBox.Show("Malzeme başarıyla eklendi.");
                _form1.MalzemeleriYukle();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
