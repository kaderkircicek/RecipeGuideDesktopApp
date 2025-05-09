using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace yemektarifuyg
{
    public partial class TarifGuncellemeForm : Form
    {
        private int TarifID;
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";
        private AnaSayfa form1;
        private string secilenResimYolu;


        public TarifGuncellemeForm(int tarifID, string tarifAdi, string kategori, int hazirlamaSuresi, string talimatlar, AnaSayfa form1Instance)
        {
            InitializeComponent();
            form1 = form1Instance;
            this.TarifID = tarifID;
            txtTarifAdi.Text = tarifAdi;
            kategoricomboBox.Text = kategori;
            txtHazirlamaSuresi.Text = hazirlamaSuresi.ToString();
            talimatrichTextBox.Text = talimatlar;


            ResmiGoster();
            MalzemeleriGetir();

        }


        private void ResmiGoster()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tarifin ResimYolu'nu çekmek için sorgu
                    string query = "SELECT ResimYolu FROM Tarifler WHERE TarifID = @TarifID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TarifID", TarifID);
                        string resimYolu = (string)command.ExecuteScalar(); // Resim yolunu al

                        if (!string.IsNullOrEmpty(resimYolu))
                        {
                            // Eğer geçerli bir resim yolu varsa resmi PictureBox'a yükle
                            pictureBox1.Image = Image.FromFile(resimYolu);
                        }
                        else
                        {
                            MessageBox.Show("Resim bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }



        // Veritabanından malzemeleri ve mevcut malzemeleri getirip checklistbox'a yükleme
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
                            clbMalzemeler.Items.Add(malzemeAdi);
                        }
                        reader.Close();
                    }

                    // Bu tarifin malzemelerini işaretle
                    query = "SELECT MalzemeID FROM TarifMalzemeIliskisi WHERE TarifID = @TarifID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TarifID", TarifID);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int malzemeID = reader.GetInt32(0);

                            // Mevcut malzemenin listede işaretlenmesi
                            for (int i = 0; i < clbMalzemeler.Items.Count; i++)
                            {
                                string malzemeAdi = clbMalzemeler.Items[i].ToString();

                                // MalzemeID'yi ad ile eşleştirmek için veritabanından tekrar kontrol yapıyoruz.
                                using (SqlCommand findCommand = new SqlCommand("SELECT MalzemeAdi FROM Malzemeler WHERE MalzemeID = @MalzemeID", connection))
                                {
                                    findCommand.Parameters.AddWithValue("@MalzemeID", malzemeID);
                                    string mevcutMalzemeAdi = (string)findCommand.ExecuteScalar();

                                    if (mevcutMalzemeAdi == malzemeAdi)
                                    {
                                        clbMalzemeler.SetItemChecked(i, true);
                                        break;
                                    }
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Mevcut malzeme ilişkilerini güncelle
                UpdateTarifMalzemeIliskisi(connection);

                // Yeni malzeme ekleme formuna yönlendir
                YeniMalzemeEklemeForm yeniMalzemeForm = new YeniMalzemeEklemeForm(TarifID);
                yeniMalzemeForm.ShowDialog();

                // Yeni malzeme eklendiğinde malzemeleri güncelliyorum
                MalzemeleriGetir();

                // Malzeme ilişkisinin güncellenmesini sağlıyoruz
                UpdateTarifMalzemeIliskisi(connection);

                form1.FormdaTarifleriGosterme();
                form1.MalzemeleriYukle();
            }
        }


        private void malzemeozellikleriguncellebutton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                UpdateTarifMalzemeIliskisi(connection);
            }
            MalzemeGuncellemeForm malzemeGuncelleForm = new MalzemeGuncellemeForm(TarifID);
            malzemeGuncelleForm.ShowDialog();
            form1.FormdaTarifleriGosterme();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // 1. Girilen tarif adının varlığını kontrol ediyorum.
                    string checkTarifQuery = "SELECT COUNT(*) FROM Tarifler WHERE TarifAdi = @TarifAdi AND TarifID <> @TarifID";
                    using (SqlCommand checkCommand = new SqlCommand(checkTarifQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@TarifAdi", txtTarifAdi.Text);
                        checkCommand.Parameters.AddWithValue("@TarifID", TarifID); // Güncellenen tarifin ID'sini ekle

                        int count = (int)checkCommand.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Girilen tarif adına ait bir tarif bulunmakta, yeni isim denemelisiniz.");
                            return;
                        }
                    }

                    // 2. Tarifler tablosunda güncelleme yapıyorum.
                    string updateTarifQuery = "UPDATE Tarifler SET TarifAdi = @TarifAdi, Kategori = @Kategori, HazirlamaSuresi = @HazirlamaSuresi, Talimatlar = @Talimatlar WHERE TarifID = @TarifID";
                    using (SqlCommand command = new SqlCommand(updateTarifQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TarifAdi", txtTarifAdi.Text);
                        command.Parameters.AddWithValue("@Kategori", kategoricomboBox.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@HazirlamaSuresi", int.Parse(txtHazirlamaSuresi.Text));
                        command.Parameters.AddWithValue("@Talimatlar", talimatrichTextBox.Text);
                        command.Parameters.AddWithValue("@TarifID", TarifID);

                        command.ExecuteNonQuery();
                    }

                    // 3. TarifMalzemeIliskisi tablosunu güncelliyorum.
                    UpdateTarifMalzemeIliskisi(connection);


                    MessageBox.Show("Tarif başarıyla güncellendi.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

            form1.FormdaTarifleriGosterme();

        }


        private void UpdateTarifMalzemeIliskisi(SqlConnection connection)
        {

            //formda işaretli olan malzemeleri buluyorum.
            List<int> selectedMalzemeIds = new List<int>();
            for (int i = 0; i < clbMalzemeler.Items.Count; i++)
            {
                if (clbMalzemeler.GetItemChecked(i))
                {
                    // İşaretli malzemenin MalzemeID'sini veritabanından buluyorum.
                    string malzemeAdi = clbMalzemeler.Items[i].ToString();
                    string getMalzemeIdQuery = "SELECT MalzemeID FROM Malzemeler WHERE MalzemeAdi = @MalzemeAdi";
                    using (SqlCommand getMalzemeIdCommand = new SqlCommand(getMalzemeIdQuery, connection))
                    {
                        getMalzemeIdCommand.Parameters.AddWithValue("@MalzemeAdi", malzemeAdi);
                        int malzemeID = (int)getMalzemeIdCommand.ExecuteScalar();
                        selectedMalzemeIds.Add(malzemeID);
                    }
                }
            }

            // Seçili malzemeleri TarifMalzemeIliskisi tablosuna ekleyelim (varsa zaten geçelim)
            foreach (int malzemeID in selectedMalzemeIds)
            {
                string checkRelationQuery = "SELECT COUNT(*) FROM TarifMalzemeIliskisi WHERE TarifID = @TarifID AND MalzemeID = @MalzemeID";
                using (SqlCommand checkRelationCommand = new SqlCommand(checkRelationQuery, connection))
                {
                    checkRelationCommand.Parameters.AddWithValue("@TarifID", TarifID);
                    checkRelationCommand.Parameters.AddWithValue("@MalzemeID", malzemeID);

                    int count = (int)checkRelationCommand.ExecuteScalar();
                    if (count == 0) // Eğer ilişki yoksa ekle
                    {
                        string insertRelationQuery = "INSERT INTO TarifMalzemeIliskisi (TarifID, MalzemeID, MalzemeMiktar) VALUES (@TarifID, @MalzemeID, @MalzemeMiktar)";
                        using (SqlCommand insertRelationCommand = new SqlCommand(insertRelationQuery, connection))
                        {
                            insertRelationCommand.Parameters.AddWithValue("@TarifID", TarifID);
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
            using (SqlCommand getCurrentMalzemelerCommand = new SqlCommand(getCurrentMalzemelerQuery, connection))
            {
                getCurrentMalzemelerCommand.Parameters.AddWithValue("@TarifID", TarifID);
                SqlDataReader reader = getCurrentMalzemelerCommand.ExecuteReader();
                while (reader.Read())
                {
                    mevcutMalzemeIds.Add(reader.GetInt32(0)); // Mevcut MalzemeID'leri ekliyoruz
                }
                reader.Close();
            }

            // Mevcut malzemelerden, kullanıcı tarafından kaldırılanları bulup siliyoruz.
            foreach (int mevcutMalzemeID in mevcutMalzemeIds)
            {
                if (!selectedMalzemeIds.Contains(mevcutMalzemeID)) // Eğer kullanıcı kaldırdıysa
                {
                    string deleteRelationQuery = "DELETE FROM TarifMalzemeIliskisi WHERE TarifID = @TarifID AND MalzemeID = @MalzemeID";
                    using (SqlCommand deleteRelationCommand = new SqlCommand(deleteRelationQuery, connection))
                    {
                        deleteRelationCommand.Parameters.AddWithValue("@TarifID", TarifID);
                        deleteRelationCommand.Parameters.AddWithValue("@MalzemeID", mevcutMalzemeID);
                        deleteRelationCommand.ExecuteNonQuery();
                    }
                }
            }

            form1.FormdaTarifleriGosterme();
        }

        private void btnResmiGuncelle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Güncellemek için bir resim seçin";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    secilenResimYolu = ofd.FileName;
                    pictureBox1.Image = Image.FromFile(secilenResimYolu); 

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            
                            string query = "UPDATE Tarifler SET ResimYolu = @ResimYolu WHERE TarifID = @TarifID";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@ResimYolu", secilenResimYolu);
                                command.Parameters.AddWithValue("@TarifID", TarifID);

                                command.ExecuteNonQuery();
                            }

                            MessageBox.Show("Resim başarıyla güncellendi.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }
    }
}