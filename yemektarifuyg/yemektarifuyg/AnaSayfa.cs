using System.Collections.Generic;
using System.Windows.Forms;
using System;
using Microsoft.Data.SqlClient;

namespace yemektarifuyg
{
    public partial class AnaSayfa : Form
    {
        public DataGridView GetDataGridView()
        {
            return dataGridView1;
        }


        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";
        public AnaSayfa()
        {
            InitializeComponent();
        }



        private void TarifEklemebutton_Click(object sender, EventArgs e)
        {
            AnaSayfaTarifEklemeForm tarifEklemeForm = new AnaSayfaTarifEklemeForm(this);
            tarifEklemeForm.ShowDialog();
        }

        private void MalzemeEklemebutton_Click(object sender, EventArgs e)
        {
            AnaSayfaMalzemeEkleme anaSayfaMalzemeEkleme = new AnaSayfaMalzemeEkleme(this);
            anaSayfaMalzemeEkleme.ShowDialog();
        }

        private void MalzemeGoruntulemebutton_Click(object sender, EventArgs e)
        {
            AnaSayfaMalzemeGoruntule malzemeGoruntule = new AnaSayfaMalzemeGoruntule();
            malzemeGoruntule.ShowDialog();
        }


        private void malzemesilmebutton_Click(object sender, EventArgs e)
        {
            AnaSayfaMalzemeSilmecs malzemeSilme = new AnaSayfaMalzemeSilmecs(this);
            malzemeSilme.ShowDialog();

        }


        private void malzemeguncellebutton_Click_1(object sender, EventArgs e)
        {
            AnaSayfaMalzemeleriGuncelle malzemeleriGuncelle = new AnaSayfaMalzemeleriGuncelle(this);
            malzemeleriGuncelle.ShowDialog();

        }

        public int GetHazirlanmaSuresi(string tarifAdi)
        {
            int hazirlamaSuresi = 0;
            string query = "SELECT HazirlamaSuresi FROM Tarifler WHERE TarifAdi = @TarifAdi";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                    hazirlamaSuresi = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return hazirlamaSuresi;
        }


        
        private float GetTarifMaliyeti(string tarifAdi)
        {
            float toplamMaliyet = 0;
            string query = "SELECT ToplamMaliyet FROM Tarifler WHERE TarifAdi = @TarifAdi";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                    toplamMaliyet = Convert.ToSingle(command.ExecuteScalar());
                }
            }
            return toplamMaliyet;
        }



        private void Form1_Load_1(object sender, EventArgs e)
        {
            FormdaTarifleriGosterme();
        }



        public void FormdaTarifleriGosterme()
        {
            MalzemeleriYukle();
            // Tarif adlar�n� MalzemeKontrol s�n�f�na g�ndererek eksik malzemenin olup olmad���n� kontrol ediyorum.
            MalzemeKontrolu malzemeKontrol = new MalzemeKontrolu(connectionString);
            List<string> tarifAdlari = GetTarifAdlari();
            ToplamMaliyetHesaplama toplamMaliyetHesaplama = new ToplamMaliyetHesaplama(connectionString);
            toplamMaliyetHesaplama.TarifToplamMaliyetHesapla(tarifAdlari);
            malzemeKontrol.KontrolEt(tarifAdlari);

            // Malzeme kontrol� tamamland�ktan sonra verileri y�kle
            TarifListeleme tarifListeleme = new TarifListeleme(connectionString);
            tarifListeleme.TarifleriGoster(tarifAdlari, dataGridView1);

        
        }



        public void MalzemeleriYukle()

        {
            malzemeeslesmecheckedListBox.Items.Clear();
            string query = "SELECT MalzemeAdi FROM Malzemeler";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            malzemeeslesmecheckedListBox.Items.Add(reader["MalzemeAdi"].ToString());
                        }
                    }
                }
            }
        }




        private List<string> GetTarifAdlari()
        {
            List<string> tarifAdlari = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TarifAdi FROM Tarifler";
                    using (SqlCommand command = new(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tarifAdlari.Add(reader["TarifAdi"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tarif adlar� al�n�rken hata: " + ex.Message);
            }

            return tarifAdlari;
        }


        private int GetTarifID(string tarifAdi)
        {
            int tarifID = -1; // Varsay�lan bir de�er
            string query = "SELECT TarifID FROM Tarifler WHERE TarifAdi = @TarifAdi";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        tarifID = Convert.ToInt32(result);
                    }
                }
            }

            return tarifID;
        }


        private void SonuclariListelebutton_Click(object sender, EventArgs e)
        {
            // Tarif adlar�n� tutmak i�in bir liste olu�turulur
            List<string> filtrelenmisTarifAdlari = new List<string>();

            // 1.Kategoriye g�re filtreleme
            string kategori = KategoricomboBox.SelectedItem != null ? KategoricomboBox.SelectedItem.ToString() : null;

            if (kategori != null)
            {
                string query = "SELECT TarifAdi FROM Tarifler WHERE Kategori = @Kategori";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Kategori", kategori);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                filtrelenmisTarifAdlari.Add(reader["TarifAdi"].ToString());
                            }
                        }
                    }
                }
                Console.WriteLine($"Kategoriye g�re filtrelenmi� tarif adlar�: {string.Join(", ", filtrelenmisTarifAdlari)}");
            }
            else
            {
                // E�er kategori se�ilmemi�se, t�m tarifleri ekle
                filtrelenmisTarifAdlari = GetTarifAdlari();
                Console.WriteLine("Kategori se�ilmedi, t�m tarifler listeye eklendi.");
            }

            // 2. Arama kutusuna g�re filtreleme
            string aramaMetni = aramatextBox.Text.Trim();
            if (!string.IsNullOrEmpty(aramaMetni))
            {
                filtrelenmisTarifAdlari = filtrelenmisTarifAdlari
                    .Where(tarif => tarif.ToLower().Contains(aramaMetni.ToLower()))
                    .ToList();

                Console.WriteLine($"Arama metnine g�re filtrelenmi� tarif adlar�: {string.Join(", ", filtrelenmisTarifAdlari)}");
            }

            // 3. Maliyet aral���na g�re filtreleme
            float minMaliyet = string.IsNullOrEmpty(minmaliyetaral�ktextbox.Text) ? 0 : float.Parse(minmaliyetaral�ktextbox.Text);
            float maxMaliyet = string.IsNullOrEmpty(maxmaliyetaral�ktextbox.Text) ? 10000 : float.Parse(maxmaliyetaral�ktextbox.Text);

            List<string> maliyetFiltreliTarifAdlari = new List<string>();
            if (filtrelenmisTarifAdlari.Count > 0)
            {
                string inClause = string.Join(",", filtrelenmisTarifAdlari.Select((tarif, index) => $"@Tarif{index}"));
                string maliyetQuery = $"SELECT TarifAdi FROM Tarifler WHERE TarifAdi IN ({inClause}) AND ToplamMaliyet BETWEEN @MinMaliyet AND @MaxMaliyet";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(maliyetQuery, connection))
                    {
                        for (int i = 0; i < filtrelenmisTarifAdlari.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@Tarif{i}", filtrelenmisTarifAdlari[i]);
                        }
                        command.Parameters.AddWithValue("@MinMaliyet", minMaliyet);
                        command.Parameters.AddWithValue("@MaxMaliyet", maxMaliyet);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                maliyetFiltreliTarifAdlari.Add(reader["TarifAdi"].ToString());
                            }
                        }
                    }
                }
                Console.WriteLine($"Maliyet aral���na g�re filtrelenmi� tarif adlar�: {string.Join(", ", maliyetFiltreliTarifAdlari)}");
            }
            else
            {
                Console.WriteLine("Maliyet filtrelemesi yap�lacak tarif yok.");
            }

            // 4. Malzeme say�s�na g�re filtreleme
            int minMalzeme = string.IsNullOrEmpty(minmalzemearal�ktextbox.Text) ? 0 : int.Parse(minmalzemearal�ktextbox.Text);
            int maxMalzeme = string.IsNullOrEmpty(maxmalzemearaliktextbox.Text) ? 10000 : int.Parse(maxmalzemearaliktextbox.Text);

            List<string> malzemeFiltreliTarifAdlari = new List<string>();
            if (maliyetFiltreliTarifAdlari.Count > 0)
            {
                string inClause = string.Join(",", maliyetFiltreliTarifAdlari.Select((tarif, index) => $"@Tarif{index}"));
                string malzemeQuery = @"
                      SELECT TarifID, COUNT(MalzemeID) AS MalzemeSayisi 
                      FROM TarifMalzemeIliskisi 
                      WHERE TarifID IN (SELECT TarifID FROM Tarifler WHERE TarifAdi IN (" + inClause + @")) 
                      GROUP BY TarifID 
                      HAVING COUNT(MalzemeID) BETWEEN @MinMalzeme AND @MaxMalzeme";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(malzemeQuery, connection))
                    {
                        for (int i = 0; i < maliyetFiltreliTarifAdlari.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@Tarif{i}", maliyetFiltreliTarifAdlari[i]);
                        }
                        command.Parameters.AddWithValue("@MinMalzeme", minMalzeme);
                        command.Parameters.AddWithValue("@MaxMalzeme", maxMalzeme);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int tarifID = (int)reader["TarifID"];
                                string tarifAdiQuery = "SELECT TarifAdi FROM Tarifler WHERE TarifID = @TarifID";
                                using (SqlCommand tarifAdiCommand = new SqlCommand(tarifAdiQuery, connection))
                                {
                                    tarifAdiCommand.Parameters.AddWithValue("@TarifID", tarifID);
                                    string tarifAdi = (string)tarifAdiCommand.ExecuteScalar();
                                    malzemeFiltreliTarifAdlari.Add(tarifAdi);
                                }
                            }
                        }
                    }
                }
                Console.WriteLine($"Malzeme say�s�na g�re filtrelenmi� tarif adlar�: {string.Join(", ", malzemeFiltreliTarifAdlari)}");
            }
            else
            {
                Console.WriteLine("Malzeme filtrelemesi yap�lacak tarif yok.");
            }




            // 5. Malzeme isimlerine g�re ek filtreleme
            string malzemeAramaMetni = malzemeismineg�rearamatextBox.Text.Trim();
            if (!string.IsNullOrEmpty(malzemeAramaMetni))
            {
                List<string> malzemeIDleri = new List<string>();

                // Malzemeler tablosundaki e�le�en malzemelerin ID'lerini al
                string malzemeQuery = "SELECT MalzemeID FROM Malzemeler WHERE MalzemeAdi LIKE @MalzemeAdi";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(malzemeQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MalzemeAdi", "%" + malzemeAramaMetni + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                malzemeIDleri.Add(reader["MalzemeID"].ToString());
                            }
                        }
                    }
                }

                // MalzemeID'lerine g�re filtreleme yap
                if (malzemeIDleri.Count > 0)
                {
                    string inClause = string.Join(",", malzemeIDleri.Select((id, index) => $"@MalzemeID{index}"));
                    string tarifFiltreQuery = @"
                    SELECT DISTINCT TarifID 
                    FROM TarifMalzemeIliskisi 
                     WHERE MalzemeID IN (" + inClause + @")";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(tarifFiltreQuery, connection))
                        {
                            for (int i = 0; i < malzemeIDleri.Count; i++)
                            {
                                command.Parameters.AddWithValue($"@MalzemeID{i}", malzemeIDleri[i]);
                            }

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                HashSet<int> uygunTarifIDs = new HashSet<int>();
                                while (reader.Read())
                                {
                                    uygunTarifIDs.Add((int)reader["TarifID"]);
                                }

                                // Uygun tarifleri filtrele
                                malzemeFiltreliTarifAdlari = malzemeFiltreliTarifAdlari
                                    .Where(tarif => uygunTarifIDs.Contains(GetTarifID(tarif)))
                                    .ToList();
                            }
                        }
                    }

                    Console.WriteLine($"Malzeme ismine g�re filtrelenmi� tarif adlar�: {string.Join(", ", malzemeFiltreliTarifAdlari)}");
                }
                else
                {
                    Console.WriteLine("Verilen malzeme ismine g�re e�le�en malzeme bulunamad�.");
                }
            }
            else
            {
                Console.WriteLine("Malzeme ismi girmediniz, bu nedenle bu filtre uygulanmad�.");
            }

            // 6. S�ralama ��lemi
            var sortedTarifler = malzemeFiltreliTarifAdlari.AsQueryable();

            switch (siralamacomboBox.SelectedIndex)
            {
                case 0: // Haz�rlanma S�resi (en h�zl�dan en yava�a)
                    sortedTarifler = sortedTarifler.OrderBy(tarif => GetHazirlanmaSuresi(tarif)).ThenBy(tarif => tarif);
                    break;
                case 1: // Haz�rlanma S�resi (en yava�tan en h�zl�ya)
                    sortedTarifler = sortedTarifler.OrderByDescending(tarif => GetHazirlanmaSuresi(tarif)).ThenBy(tarif => tarif);
                    break;
                case 2: // Tarif Maliyeti (artandan azalana)
                    sortedTarifler = sortedTarifler.OrderByDescending(tarif => GetTarifMaliyeti(tarif)).ThenBy(tarif => tarif);
                    break;
                case 3: // Tarif Maliyeti (azalandan artana)
                    sortedTarifler = sortedTarifler.OrderBy(tarif => GetTarifMaliyeti(tarif)).ThenBy(tarif => tarif);
                    break;
                default:
                    break;
            }

            // 8. S�ralanm�� tarifleri konsola yazd�r
            Console.WriteLine("S�ralanm�� Tarifler:");
            foreach (var tarif in sortedTarifler)
            {
                Console.WriteLine(tarif);
            }


            if (sortedTarifler.Any())
            {


                TarifListeleme tarifListeleme = new TarifListeleme(connectionString);
                tarifListeleme.TarifleriGoster3(sortedTarifler.ToList(), dataGridView1);
            }
            else
            {
                MessageBox.Show("G�sterilecek tarif bulunamad�.");
            }
        }

        private void malzemeyeGoreAraButton_Click(object sender, EventArgs e)
        {
            List<string> secilenMalzemeler = malzemeeslesmecheckedListBox.CheckedItems.Cast<string>().ToList();

            if (secilenMalzemeler.Count == 0)
            {
                MessageBox.Show("L�tfen malzeme se�in.");
                return;
            }

            List<TarifEslesmeSonucu> eslesmeSonuclari = new List<TarifEslesmeSonucu>();

            string malzemeInClause = string.Join(",", secilenMalzemeler.Select((m, i) => $"@Malzeme{i}"));
            string query = @"
             SELECT t.TarifID, t.TarifAdi, COUNT(tm.MalzemeID) AS EslesenMalzemeSayisi, 
             (SELECT COUNT(MalzemeID) FROM TarifMalzemeIliskisi WHERE TarifID = t.TarifID) AS ToplamMalzeme
             FROM Tarifler t
             JOIN TarifMalzemeIliskisi tm ON t.TarifID = tm.TarifID
             JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID
             WHERE m.MalzemeAdi IN (" + malzemeInClause + @")
             GROUP BY t.TarifID, t.TarifAdi";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    for (int i = 0; i < secilenMalzemeler.Count; i++)
                    {
                        command.Parameters.AddWithValue($"@Malzeme{i}", secilenMalzemeler[i]);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int eslesenMalzemeSayisi = (int)reader["EslesenMalzemeSayisi"];
                            int toplamMalzeme = (int)reader["ToplamMalzeme"];
                            float eslesmeYuzdesi = (float)eslesenMalzemeSayisi / toplamMalzeme * 100;

                            eslesmeSonuclari.Add(new TarifEslesmeSonucu
                            {
                                TarifAdi = reader["TarifAdi"].ToString(),
                                EslesmeYuzdesi = eslesmeYuzdesi
                            });
                        }
                    }
                }
            }

            // �lk �nce e�le�me y�zdesine g�re azalan s�ralama
            eslesmeSonuclari = eslesmeSonuclari.OrderByDescending(sonuc => sonuc.EslesmeYuzdesi).ToList();


            // Ayn� e�le�me y�zdesine sahip tarifleri alfabetik olarak s�ralama yap�yorum.
            eslesmeSonuclari = eslesmeSonuclari
                .GroupBy(sonuc => sonuc.EslesmeYuzdesi)
                .SelectMany(grup => grup.OrderBy(sonuc => sonuc.TarifAdi)) 
                .ToList();

           
            if (!eslesmeSonuclari.Any())
            {
                MessageBox.Show("E�le�en tarif bulunamad�.");
                return; 
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                foreach (var sonuc in eslesmeSonuclari)
                {
                    string updateQuery = "UPDATE Tarifler SET EslesmeYuzdesi = @EslesmeYuzdesi WHERE TarifAdi = @TarifAdi";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@EslesmeYuzdesi", sonuc.EslesmeYuzdesi);
                        updateCommand.Parameters.AddWithValue("@TarifAdi", sonuc.TarifAdi);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }


            List<string> eslesenTarifAdlari = eslesmeSonuclari.Select(sonuc => sonuc.TarifAdi).ToList();

            // Tarifleri g�ster
            TarifListeleme tarifListeleme = new TarifListeleme(connectionString);
            tarifListeleme.TarifleriGoster2(eslesenTarifAdlari, dataGridView1);
        }

        public class TarifEslesmeSonucu
        {
            public string TarifAdi { get; set; }
            public float EslesmeYuzdesi { get; set; }
        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            // Tarif Ad� s�tununa t�klan�p t�klanmad���n� kontrol ediyorum
            if (e.ColumnIndex == dataGridView1.Columns["TarifAdi"].Index && e.RowIndex >= 0)
            {

                string selectedTarifAdi = dataGridView1.Rows[e.RowIndex].Cells["TarifAdi"].Value.ToString();

                TarifDetaylariniG�ster(selectedTarifAdi);
            }
        }


        private void TarifDetaylariniG�ster(string tarifAdi)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string query = $"SELECT TarifID, Kategori, HazirlamaSuresi, Talimatlar, ResimYolu FROM Tarifler WHERE TarifAdi = @TarifAdi";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TarifAdi", tarifAdi);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int tarifID = reader.GetInt32(0);
                        string kategori = reader.GetString(1);
                        int hazirlamaSuresi = reader.GetInt32(2);
                        string talimatlar = reader.GetString(3);
                        string resimYolu = reader.IsDBNull(4) ? null : reader.GetString(4);


                        TarifDetaylar�n�Y�kle(tarifID, tarifAdi, kategori, hazirlamaSuresi, talimatlar, resimYolu);
                    }
                    else
                    {
                        MessageBox.Show("Tarif bulunamad�.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void TarifDetaylar�n�Y�kle(int tarifID, string tarifAdi, string kategori, int hazirlamaSuresi, string talimatlar, string resimYolu)
        {
            TarifDetayForm detayForm = new TarifDetayForm(this);


            detayForm.TarifID = tarifID;
            detayForm.TarifAdi = tarifAdi;
            detayForm.Kategori = kategori;
            detayForm.HazirlamaSuresi = hazirlamaSuresi;
            detayForm.Talimatlar = talimatlar;

            // Resmi y�kle
            if (!string.IsNullOrEmpty(resimYolu) && File.Exists(resimYolu))
            {
                try
                {
                    Bitmap img = new Bitmap(resimYolu);
                    detayForm.Resim = img;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Resim y�klenemedi.");
                }
            }

            // Detay formunu g�ster
            detayForm.ShowDialog();
        }

        
    }
}
