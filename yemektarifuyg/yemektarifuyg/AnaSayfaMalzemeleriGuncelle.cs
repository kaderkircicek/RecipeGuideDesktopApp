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
    public partial class AnaSayfaMalzemeleriGuncelle : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True;");
        private AnaSayfa _form1;
        public AnaSayfaMalzemeleriGuncelle(AnaSayfa form1)
        {
            InitializeComponent();
            _form1 = form1;
        }


        private void MalzemeleriGuncelle_Load_1(object sender, EventArgs e)
        {
            LoadMalzemeler();
        }


        private void LoadMalzemeler()
        {
            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Malzemeler", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.ReadOnly = false; // Düzenlenebilir hale getirme
                dataGridView1.Columns["MalzemeID"].ReadOnly = true; // Primary Key olan ID düzenlenemez

                // Sütun başlıklarını kullanıcıya daha anlaşılır hale getirme
                dataGridView1.Columns["MalzemeID"].HeaderText = "Malzeme ID";
                dataGridView1.Columns["MalzemeAdi"].HeaderText = "Malzeme Adı";
                dataGridView1.Columns["ToplamMiktar"].HeaderText = "Toplam Miktar";
                dataGridView1.Columns["MalzemeBirim"].HeaderText = "Birim";
                dataGridView1.Columns["BirimFiyat"].HeaderText = "Birim Fiyat";

                // Sütun genişliklerini ayarlama
              
                dataGridView1.Columns["MalzemeAdi"].Width = 175; // MalzemeAdi sütunu genişliği
                dataGridView1.Columns["ToplamMiktar"].Width = 150; // ToplamMiktar sütunu genişliği
                dataGridView1.Columns["MalzemeBirim"].Width = 150; // MalzemeBirim sütunu genişliği
                dataGridView1.Columns["BirimFiyat"].Width = 150; // BirimFiyat sütunu genişliği

                dataGridView1.Columns["MalzemeID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme hatası: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue; 

                    SqlCommand cmd = new SqlCommand("UPDATE Malzemeler SET MalzemeAdi=@MalzemeAdi, ToplamMiktar=@ToplamMiktar, MalzemeBirim=@MalzemeBirim, BirimFiyat=@BirimFiyat WHERE MalzemeID=@MalzemeID", connection);
                    cmd.Parameters.AddWithValue("@MalzemeID", row.Cells["MalzemeID"].Value);
                    cmd.Parameters.AddWithValue("@MalzemeAdi", row.Cells["MalzemeAdi"].Value);
                    cmd.Parameters.AddWithValue("@ToplamMiktar", row.Cells["ToplamMiktar"].Value);
                    cmd.Parameters.AddWithValue("@MalzemeBirim", row.Cells["MalzemeBirim"].Value);
                    cmd.Parameters.AddWithValue("@BirimFiyat", row.Cells["BirimFiyat"].Value);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Güncellemeler başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası: " + ex.Message);
            }
            finally
            {
                connection.Close();
                LoadMalzemeler(); 
                _form1.FormdaTarifleriGosterme();
            }
        }

       
    }
}
