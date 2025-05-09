using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace yemektarifuyg
{
    internal class TarifListeleme
    {
        private string connectionString;

        public TarifListeleme(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void TarifleriGoster(List<string> tarifAdlari, DataGridView dataGridView2)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tarif adlarına göre filtreleme yaparak sorguyu oluşturuyorum
                    string tarifAdlariListesi = string.Join(",", tarifAdlari.Select(t => $"'{t}'"));
                    string query = $"SELECT TarifAdi, Kategori, HazirlamaSuresi, ResimYolu, EksikMalzemeVarMi, ToplamMaliyet, EksikMalzemeMaliyeti FROM Tarifler WHERE TarifAdi IN ({tarifAdlariListesi})";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Kategoriye özel sıralama yapma
                    var kategoriSira = new List<string> { "Çorba", "Ara Sıcak", "Ana Yemek", "Meze", "Tatlı" };
                    var sortedRows = dataTable.AsEnumerable()
                        .OrderBy(row => kategoriSira.IndexOf(row.Field<string>("Kategori")))  // Kategori sıralaması
                        .ThenBy(row => row.Field<string>("TarifAdi"))                        // Aynı kategoride alfabetik sıralama
                        .CopyToDataTable();

                    // DataGridView'e sıralanmış veriyi ata
                    dataGridView2.DataSource = sortedRows;

                    // Sütun başlıklarını düzenleme
                    dataGridView2.Columns["TarifAdi"].HeaderText = "Tarif Adı";
                    dataGridView2.Columns["Kategori"].HeaderText = "Kategorisi";
                    dataGridView2.Columns["HazirlamaSuresi"].HeaderText = "Hazırlama Süresi";
                    dataGridView2.Columns["ToplamMaliyet"].HeaderText = "Toplam Maliyet";
                    dataGridView2.Columns["EksikMalzemeMaliyeti"].HeaderText = "Eksik Malzeme Maliyeti";

                    // "Tarif Resmi" sütununu ekleyin ve ilk sütuna yerleştirin
                    if (!dataGridView2.Columns.Contains("Tarif Resmi"))
                    {
                        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
                        {
                            Name = "Tarif Resmi",
                            HeaderText = "Tarif Resmi",
                            ImageLayout = DataGridViewImageCellLayout.Zoom
                        };
                        dataGridView2.Columns.Insert(0, imageColumn);  // İlk sütun olarak ekleyin
                    }

                    // Sütun sıralamasını belirleme
                    dataGridView2.Columns["TarifAdi"].DisplayIndex = 1;
                    dataGridView2.Columns["Kategori"].DisplayIndex = 2;
                    dataGridView2.Columns["HazirlamaSuresi"].DisplayIndex = 3;
                    dataGridView2.Columns["ToplamMaliyet"].DisplayIndex = 4;
                    dataGridView2.Columns["EksikMalzemeMaliyeti"].DisplayIndex = 5;

                  

                    // Sütun genişliklerini ayarla (sabit olarak belirlenmiş genişlikler)
                    dataGridView2.Columns["TarifAdi"].Width = 200;
                    dataGridView2.Columns["Kategori"].Width = 150;
                    dataGridView2.Columns["HazirlamaSuresi"].Width = 150;
                    dataGridView2.Columns["ToplamMaliyet"].Width = 150;
                    dataGridView2.Columns["EksikMalzemeMaliyeti"].Width= 160;


                    dataGridView2.RowTemplate.Height = 125;

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            ResmiYukle(row);
                            SatirlariBoya(row);
                        }
                    }

                    // ResimYolu ve EksikMalzemeVarMi sütunlarını gizle
                    dataGridView2.Columns["ResimYolu"].Visible = false;
                    dataGridView2.Columns["EksikMalzemeVarMi"].Visible = false;

                    //Son satırı gizle, eğer yeni satır varsa
                    dataGridView2.AllowUserToAddRows = false; // Yeni satır eklemeyi engelle
                    if (dataGridView2.Rows.Count > 0 && dataGridView2.IsCurrentRowDirty)
                    {
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Visible = false; // Son satırı gizle
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ResmiYukle(DataGridViewRow row)
        {
            if (row.Cells["ResimYolu"].Value != null && !string.IsNullOrEmpty(row.Cells["ResimYolu"].Value.ToString()))
            {
                string imagePath = row.Cells["ResimYolu"].Value.ToString();

                if (File.Exists(imagePath))
                {
                    try
                    {
                        using (Bitmap img = new Bitmap(imagePath))
                        {
                            // Resmin boyutlarını orantılı olarak ayarla
                            int maxWidth = 100;
                            int maxHeight = 100;
                            double ratio = Math.Min((double)maxWidth / img.Width, (double)maxHeight / img.Height);
                            int newWidth = (int)(img.Width * ratio);
                            int newHeight = (int)(img.Height * ratio);

                            // Resmi yeniden boyutlandır ve hücreye ekle
                            Image resizedImage = new Bitmap(img, new Size(newWidth, newHeight));
                            row.Cells["Tarif Resmi"].Value = resizedImage;
                            row.Height = newHeight + 20; // Satır yüksekliğini resme göre ayarla
                        }
                    }
                    catch (ArgumentException argEx)
                    {
                        MessageBox.Show("Resim yükleme hatası: " + argEx.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Resim bulunamadı: " + imagePath);
                }
            }
        }


        private void SatirlariBoya(DataGridViewRow row)
        {
            if (row.Cells["EksikMalzemeVarMi"].Value != null)
            {
                int eksikMalzemeDurumu = Convert.ToInt32(row.Cells["EksikMalzemeVarMi"].Value);

                // Eksik malzeme varsa (EksikMalzemeVarMi == 1), satır kırmızıya boyanmalı
                if (eksikMalzemeDurumu == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
                // Eksik malzeme yoksa (EksikMalzemeVarMi == 0), satır yeşile boyanmalı
                else if (eksikMalzemeDurumu == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        public void TarifleriGoster2(List<string> tarifAdlari, DataGridView dataGridView2)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string tarifAdlariListesi = string.Join(",", tarifAdlari.Select(t => $"'{t}'"));
                    string query = $"SELECT TarifAdi,Kategori, HazirlamaSuresi, ResimYolu, EksikMalzemeVarMi, ToplamMaliyet, EslesmeYuzdesi FROM Tarifler WHERE TarifAdi IN ({tarifAdlariListesi})";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    if (!dataGridView2.Columns.Contains("Tarif Resmi"))
                    {
                        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
                        {
                            Name = "Tarif Resmi",
                            HeaderText = "Tarif Resmi",
                            ImageLayout = DataGridViewImageCellLayout.Zoom
                        };
                        dataGridView2.Columns.Add(imageColumn);
                    }

                    dataGridView2.DataSource = dataTable;

                    // Sütun genişliklerini ayarla (sabit olarak belirlenmiş genişlikler)
                    dataGridView2.Columns["TarifAdi"].Width = 200;
                    dataGridView2.Columns["Kategori"].Width = 150;
                    dataGridView2.Columns["HazirlamaSuresi"].Width = 175;
                    dataGridView2.Columns["ToplamMaliyet"].Width = 125;
                    dataGridView2.Columns["EslesmeYuzdesi"].Width = 125;

                    dataGridView2.RowTemplate.Height = 125;

                    // Sütun başlıklarını düzenleme
                    dataGridView2.Columns["TarifAdi"].HeaderText = "Tarif Adı";
                    dataGridView2.Columns["Kategori"].HeaderText = "Kategori";
                    dataGridView2.Columns["HazirlamaSuresi"].HeaderText = "Hazırlama Süresi";
                    dataGridView2.Columns["ToplamMaliyet"].HeaderText = "Toplam Maliyet";
                    dataGridView2.Columns["EslesmeYuzdesi"].HeaderText = "Eşleşme Yüzdesi";

                    var sortedRows = dataTable.AsEnumerable()
                        .OrderBy(row => tarifAdlari.IndexOf(row.Field<string>("TarifAdi")))
                        .CopyToDataTable();

                    dataGridView2.DataSource = sortedRows;

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            ResmiYukle(row);
                            SatirlariBoya(row);
                        }
                    }

                    // ResimYolu ve EksikMalzemeVarMi sütunlarını gizle
                    dataGridView2.Columns["ResimYolu"].Visible = false;
                    dataGridView2.Columns["EksikMalzemeVarMi"].Visible = false;

                    // Son satırı gizle, eğer yeni satır varsa
                    dataGridView2.AllowUserToAddRows = false; // Yeni satır eklemeyi engelle
                    if (dataGridView2.Rows.Count > 0 && dataGridView2.IsCurrentRowDirty)
                    {
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Visible = false; // Son satırı gizle
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        public void TarifleriGoster3(List<string> tarifAdlari, DataGridView dataGridView2)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tarif adlarına göre filtreleme yaparak sorguyu oluşturuyorum
                    string tarifAdlariListesi = string.Join(",", tarifAdlari.Select(t => $"'{t}'"));
                    string query = $"SELECT TarifAdi, Kategori, HazirlamaSuresi, ResimYolu, EksikMalzemeVarMi, ToplamMaliyet, EksikMalzemeMaliyeti FROM Tarifler WHERE TarifAdi IN ({tarifAdlariListesi})";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    
                    if (!dataGridView2.Columns.Contains("Tarif Resmi"))
                    {
                        DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
                        {
                            Name = "Tarif Resmi",
                            HeaderText = "Tarif Resmi",
                            ImageLayout = DataGridViewImageCellLayout.Zoom
                        };
                        dataGridView2.Columns.Add(imageColumn);
                    }

                    dataGridView2.DataSource = dataTable;


                    // Sütun genişliklerini ayarla (sabit olarak belirlenmiş genişlikler)
                    dataGridView2.Columns["TarifAdi"].Width = 200;
                    dataGridView2.Columns["Kategori"].Width = 150;
                    dataGridView2.Columns["HazirlamaSuresi"].Width = 160;
                    dataGridView2.Columns["ToplamMaliyet"].Width = 125;
                    dataGridView2.Columns["EksikMalzemeMaliyeti"].Width = 160;

                    dataGridView2.RowTemplate.Height = 125;

                
                    dataGridView2.Columns["TarifAdi"].HeaderText = "Tarif Adı";
                    dataGridView2.Columns["Kategori"].HeaderText = "Kategori";
                    dataGridView2.Columns["HazirlamaSuresi"].HeaderText = "Hazırlama Süresi";
                    dataGridView2.Columns["ToplamMaliyet"].HeaderText = "Toplam Maliyet";
                    dataGridView2.Columns["EksikMalzemeMaliyeti"].HeaderText = "Eksik Malzeme Maliyeti";

                    
                    var sortedRows = dataTable.AsEnumerable()
                        .OrderBy(row => tarifAdlari.IndexOf(row.Field<string>("TarifAdi")))
                        .CopyToDataTable();

                    dataGridView2.DataSource = sortedRows;

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            ResmiYukle(row);
                            SatirlariBoya(row);
                        }
                    }

                    
                    dataGridView2.Columns["ResimYolu"].Visible = false;
                    dataGridView2.Columns["EksikMalzemeVarMi"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }





    }
}
