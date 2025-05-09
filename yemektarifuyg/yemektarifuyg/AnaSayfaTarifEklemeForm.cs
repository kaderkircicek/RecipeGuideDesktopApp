using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing; 
using System.IO;

namespace yemektarifuyg
{
    public partial class AnaSayfaTarifEklemeForm : Form
    {
        public int tarifID; 
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True;";
        private List<int> secilenMalzemeIDListesi = new List<int>(); 
        private AnaSayfa _form1;
        private string secilenResimYolu;

        public AnaSayfaTarifEklemeForm(AnaSayfa form1)
        {
            InitializeComponent();
            MalzemeleriGetir();
            _form1 = form1; 
        }

        private void btntarifiKaydet_Click(object sender, EventArgs e)
        {
            
            string tarifAdi = txtTarifAdi.Text.Trim();
            string kategori = kategoricomboBox.SelectedItem?.ToString();
            int hazirlamaSuresi;
            string talimatlar = talimatrichTextBox.Text.Trim();
            string resimYolu = string.Empty;

            // Hazırlama süresinin geçerli bir değer olup olmadığını kontrol ediyorum.
            if (!int.TryParse(txtHazirlamaSuresi.Text, out hazirlamaSuresi))
            {
                MessageBox.Show("Lütfen geçerli bir hazırlama süresi girin.");
                return;
            }

            // Zorunlu alanların dolu olduğunu kontrol ediyorum.
            if (string.IsNullOrEmpty(tarifAdi) || string.IsNullOrEmpty(kategori) || string.IsNullOrEmpty(talimatlar))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }


           
            if (TarifAdiMevcutMu(tarifAdi))
            {
                MessageBox.Show("Bu tarif adı zaten mevcut. Lütfen yeni bir tarif adı girin.");
                return;
            }

           
            if (pictureBoxResim.Image != null) 
            {
                
                resimYolu = secilenResimYolu;
            }
            else
            {
                MessageBox.Show("Tarifiniz resim eklemeden kaydedilmiştir.");
                // Resim yolu boşsa, boş string olarak geçmek için ayarlıyorum.
                resimYolu = string.Empty;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string query = @"INSERT INTO Tarifler (TarifAdi, Kategori, HazirlamaSuresi, Talimatlar, ResimYolu)
                             VALUES (@TarifAdi, @Kategori, @HazirlamaSuresi, @Talimatlar, @ResimYolu);
                             SELECT SCOPE_IDENTITY();"; 

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                        command.Parameters.AddWithValue("@Kategori", kategori);
                        command.Parameters.AddWithValue("@HazirlamaSuresi", hazirlamaSuresi);
                        command.Parameters.AddWithValue("@Talimatlar", talimatlar);
                        command.Parameters.AddWithValue("@ResimYolu", resimYolu); // Resim yolunu kaydediyoruz

                        tarifID = Convert.ToInt32(command.ExecuteScalar());

                        MessageBox.Show("Tarif başarıyla kaydedildi.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

            _form1.FormdaTarifleriGosterme(); // Form1'deki metodu çağır
            _form1.MalzemeleriYukle();

        }



        private bool TarifAdiMevcutMu(string tarifAdi)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Tarifler WHERE TarifAdi = @TarifAdi";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                    int count = (int)command.ExecuteScalar();
                    return count > 0; 
                }
            }
        }


        // Veritabanından malzemeleri ve mevcut malzemeleri getirip checklistbox'a yüklüyorum.
        private void MalzemeleriGetir()
        {
            clbMalzemeler.Items.Clear(); // Listeyi temizliyoruz ki iki kere eklenmesin
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tüm malzemeleri al ve sadece malzeme adlarını checklistbox'a ekle
                    string query = "SELECT MalzemeID, MalzemeAdi FROM Malzemeler";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int malzemeID = reader.GetInt32(0);
                            string malzemeAdi = reader.GetString(1);

                            // Malzeme ID ve adı ile bir nesne ekliyoruz
                            clbMalzemeItem malzemeItem = new clbMalzemeItem { MalzemeID = malzemeID, MalzemeAdi = malzemeAdi };
                            clbMalzemeler.Items.Add(malzemeItem);
                        }
                        reader.Close();
                    }

                    // Bu tarifin malzemelerini işaretle
                    query = "SELECT MalzemeID FROM TarifMalzemeIliskisi WHERE TarifID = @TarifID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TarifID", tarifID);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int malzemeID = reader.GetInt32(0);

                            // Mevcut malzemenin listede işaretlenmesi
                            for (int i = 0; i < clbMalzemeler.Items.Count; i++)
                            {
                                var malzemeItem = (clbMalzemeItem)clbMalzemeler.Items[i];
                                if (malzemeItem.MalzemeID == malzemeID)
                                {
                                    clbMalzemeler.SetItemChecked(i, true); // İlgili malzemeyi işaretle
                                    break;
                                }
                            }
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void yenimalzemeeklebutton_Click(object sender, EventArgs e)
        {
            MalzemeTarifIliskilendirme();


            YeniMalzemeEklemeForm yeniMalzemeForm = new YeniMalzemeEklemeForm(tarifID);
            yeniMalzemeForm.ShowDialog();

            MalzemeleriGetir();
            _form1.FormdaTarifleriGosterme();
            _form1.MalzemeleriYukle();

            MalzemeTarifIliskilendirme();
        }

        private void malzememiktarıgirbutton_Click(object sender, EventArgs e)
        {
            // Önce tarifin eklenip eklenmediğini kontrol ediyorum
            if (tarifID <= 0) // tarifID sıfır veya daha küçükse
            {
                MessageBox.Show("Lütfen önce tarif ekleyiniz."); // Kullanıcıya mesaj göster
                return; // Fonksiyondan çık
            }

            MalzemeTarifIliskilendirme();
            secilenMalzemeIDListesi.Clear(); // Listeyi temizle


            // Seçilen malzemeleri listeye ekliyorum.
            foreach (clbMalzemeItem item in clbMalzemeler.CheckedItems)
            {
                secilenMalzemeIDListesi.Add(item.MalzemeID);
            }

            if (tarifID > 0 && secilenMalzemeIDListesi.Count > 0)
            {
                
                MalzemeMiktarıGir miktarForm = new MalzemeMiktarıGir(tarifID, secilenMalzemeIDListesi);
                miktarForm.Show();
            }
            else
            {
                MessageBox.Show("Lütfen tarifi kaydedin ve en az bir malzeme seçin.");
            }

            _form1.FormdaTarifleriGosterme(); 
        }

        private void malzemeiliskilerikaydet_Click(object sender, EventArgs e)
        {
            MalzemeTarifIliskilendirme();
            MessageBox.Show("Malzemeler Başarıyla kaydedildi.");
            _form1.FormdaTarifleriGosterme();
        }


        public void MalzemeTarifIliskilendirme()
        {
            List<int> selectedMalzemeIds = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Seçili malzemeleri belirleyelim
                for (int i = 0; i < clbMalzemeler.Items.Count; i++)
                {
                    if (clbMalzemeler.GetItemChecked(i))
                    {
                        // İşaretli malzemenin ID'sini ekleyelim
                        var selectedMalzeme = (clbMalzemeItem)clbMalzemeler.Items[i];
                        selectedMalzemeIds.Add(selectedMalzeme.MalzemeID);
                    }
                }

                // Seçili malzemeleri TarifMalzemeIliskisi tablosuna ekliyorum (varsa zaten geçiyorum.)
                foreach (int malzemeID in selectedMalzemeIds)
                {
                    string checkRelationQuery = "SELECT COUNT(*) FROM TarifMalzemeIliskisi WHERE TarifID = @TarifID AND MalzemeID = @MalzemeID";
                    using (SqlCommand checkRelationCommand = new SqlCommand(checkRelationQuery, connection))
                    {
                        checkRelationCommand.Parameters.AddWithValue("@TarifID", tarifID);
                        checkRelationCommand.Parameters.AddWithValue("@MalzemeID", malzemeID);

                        int count = (int)checkRelationCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            string insertRelationQuery = "INSERT INTO TarifMalzemeIliskisi (TarifID, MalzemeID, MalzemeMiktar) VALUES (@TarifID, @MalzemeID, @MalzemeMiktar)";
                            using (SqlCommand insertRelationCommand = new SqlCommand(insertRelationQuery, connection))
                            {
                                insertRelationCommand.Parameters.AddWithValue("@TarifID", tarifID);
                                insertRelationCommand.Parameters.AddWithValue("@MalzemeID", malzemeID);
                                insertRelationCommand.Parameters.AddWithValue("@MalzemeMiktar", 1.0);

                                insertRelationCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // 3. Kullanıcının kaldırdığı malzemeleri TarifMalzemeIliskisi tablosundan silelim.
                string getCurrentMalzemelerQuery = "SELECT MalzemeID FROM TarifMalzemeIliskisi WHERE TarifID = @TarifID";
                List<int> mevcutMalzemeIds = new List<int>();

                // Mevcut malzemeleri al
                using (SqlCommand getCurrentMalzemelerCommand = new SqlCommand(getCurrentMalzemelerQuery, connection))
                {
                    getCurrentMalzemelerCommand.Parameters.AddWithValue("@TarifID", tarifID);
                    using (SqlDataReader reader = getCurrentMalzemelerCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mevcutMalzemeIds.Add(reader.GetInt32(0)); // Mevcut MalzemeID'leri ekliyoruz
                        }
                    }
                }

                // Mevcut malzemelerden, kullanıcı tarafından kaldırılanları bulup siliyoruz.
                foreach (int mevcutMalzemeID in mevcutMalzemeIds)
                {
                    if (!selectedMalzemeIds.Contains(mevcutMalzemeID)) // Eğer kullanıcı kaldırdıysa
                    {
                        string deleteRelationQuery = "DELETE FROM TarifMalzemeIliskisi WHERE TarifID = @TarifID AND MalzemeID = @MalzemeID";
                        using (SqlCommand deleteRelationCommand = new SqlCommand(deleteRelationQuery, connection))
                        {
                            deleteRelationCommand.Parameters.AddWithValue("@TarifID", tarifID);
                            deleteRelationCommand.Parameters.AddWithValue("@MalzemeID", mevcutMalzemeID);
                            deleteRelationCommand.ExecuteNonQuery();
                        }
                    }
                }
            }

            _form1.FormdaTarifleriGosterme();
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp"; 
                ofd.Title = "Bir resim seçin";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    secilenResimYolu = ofd.FileName; 
                    pictureBoxResim.Image = Image.FromFile(secilenResimYolu); 
                }
            }
        }
    }

    // clbMalzemeler için özel sınıf
    public class clbMalzemeItem
    {
        public int MalzemeID { get; set; }
        public string MalzemeAdi { get; set; }

        public override string ToString()
        {
            return MalzemeAdi;
        }
    }
}
