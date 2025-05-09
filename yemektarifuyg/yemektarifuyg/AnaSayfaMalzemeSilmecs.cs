using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace yemektarifuyg
{
    public partial class AnaSayfaMalzemeSilmecs : Form
    {
        private AnaSayfa _form1;
        public AnaSayfaMalzemeSilmecs(AnaSayfa form1)
        {
            InitializeComponent();
            LoadMalzemeler();
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            _form1 = form1; 
        }

     
        private void LoadMalzemeler()
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True;";
            string query = "SELECT MalzemeID, MalzemeAdi, ToplamMiktar, MalzemeBirim, BirimFiyat FROM Malzemeler";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Sil butonunu eklemek için yeni bir DataGridViewButtonColumn oluşturuyorum
                DataGridViewButtonColumn silButton = new DataGridViewButtonColumn();
                silButton.HeaderText = "Sil";
                silButton.Name = "Sil";
                silButton.Text = "Sil";
                silButton.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dt;


                dataGridView1.Columns["MalzemeID"].Visible = false;


                dataGridView1.Columns["MalzemeAdi"].HeaderText = "Malzeme Adı";
                dataGridView1.Columns["ToplamMiktar"].HeaderText = "Toplam Miktar";
                dataGridView1.Columns["MalzemeBirim"].HeaderText = "Birim";
                dataGridView1.Columns["BirimFiyat"].HeaderText = "Birim Fiyatı";

                // Sütun genişliklerini sabitliyorum
                dataGridView1.Columns["MalzemeAdi"].Width = 150;
                dataGridView1.Columns["ToplamMiktar"].Width = 100; 
                dataGridView1.Columns["MalzemeBirim"].Width = 80; 
                dataGridView1.Columns["BirimFiyat"].Width = 100; 
                dataGridView1.Columns.Add(silButton);


                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
        }

       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Eğer tıklanan sütun buton ise ve bir satır tıklanmışsa
            if (e.ColumnIndex == dataGridView1.Columns["Sil"].Index && e.RowIndex >= 0)
            {
                int malzemeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MalzemeID"].Value);

                
                DialogResult dialogResult = MessageBox.Show("Bu malzemeyi silmek istediğinizden emin misiniz?", "Malzeme Sil", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    MalzemeSilme(malzemeID);
                }
            }

            _form1.FormdaTarifleriGosterme();
            
        }


        private void MalzemeSilme(int malzemeID)
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // İlk olarak TarifMalzemeIliskisi tablosundan ilgili malzemenin ilişkilerini siliyorum.
                    string deleteRelationQuery = "DELETE FROM TarifMalzemeIliskisi WHERE MalzemeID = @MalzemeID";
                    using (SqlCommand cmd = new SqlCommand(deleteRelationQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MalzemeID", malzemeID);
                        cmd.ExecuteNonQuery();
                    }

                    // Ardından Malzemeler tablosundan ilgili malzemeyi siliyorum.
                    string deleteMalzemeQuery = "DELETE FROM Malzemeler WHERE MalzemeID = @MalzemeID";
                    using (SqlCommand cmd = new SqlCommand(deleteMalzemeQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MalzemeID", malzemeID);
                        cmd.ExecuteNonQuery();
                    }

                   
                    transaction.Commit();

                    // Silme işlemi başarıyla tamamlandı mesajı
                    MessageBox.Show("Malzeme başarıyla silindi.", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Malzeme listesi güncellensin
                    LoadMalzemeler();
                    _form1.MalzemeleriYukle();
                }
                catch (Exception ex)
                {
                    // Hata meydana gelirse işlemi geri al
                    transaction.Rollback();
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
