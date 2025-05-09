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
    public partial class TarifDetayForm : Form
    {
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";
        private AnaSayfa form1;
        public int TarifID { get; set; }
        public string TarifAdi { get; set; }
        public string Kategori { get; set; }
        public int HazirlamaSuresi { get; set; }
        public string Talimatlar { get; set; }
        public Bitmap Resim { get; set; }
        public TarifDetayForm(AnaSayfa form1Instance)
        {
            InitializeComponent();
            form1 = form1Instance;
        }

        private void TarifDetayForm_Load(object sender, EventArgs e)
        {
            tarifadilabel.Text = TarifAdi;
            kategorilabel.Text = Kategori;
            hazirlamasure.Text = HazirlamaSuresi.ToString() + " dakika";
            talimatlarRichTextBox.Text = Talimatlar;

           
            if (Resim != null)
            {
                pictureBox1.Image = Resim;
            }

            MalzemeleriYükle();
        }


        private void MalzemeleriYükle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string query = @"
                     SELECT m.MalzemeAdi, tmi.MalzemeMiktar, m.MalzemeBirim 
                     FROM TarifMalzemeIliskisi tmi
                     INNER JOIN Malzemeler m ON tmi.MalzemeID = m.MalzemeID
                      WHERE tmi.TarifID = @TarifID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TarifID", TarifID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                        
                            while (reader.Read())
                            {
                                string malzemeAdi = reader["MalzemeAdi"].ToString();
                                string malzemeMiktar = reader["MalzemeMiktar"].ToString();
                                string birim = reader["MalzemeBirim"].ToString();

                               
                                malzemerichTextBox1.AppendText($"{malzemeAdi}: {malzemeMiktar} {birim}{Environment.NewLine}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Malzemeler yüklenirken hata: " + ex.Message);
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
                    using (SqlCommand command = new SqlCommand(query, connection))
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
                MessageBox.Show("Tarif adları alınırken hata: " + ex.Message);
            }

            return tarifAdlari;
        }

        private void tarifisilbutton_Click(object sender, EventArgs e)
        {
            // Tarifin silinmesini onaylamak için bir mesaj gösteriyoruz
            DialogResult result = MessageBox.Show("Bu tarifi silmek istediğinizden emin misiniz?", "Tarifi Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // İlişkili malzemeleri TarifMalzemeIliskisi tablosundan sil
                        string deleteIngredientsQuery = "DELETE FROM TarifMalzemeIliskisi WHERE TarifID = @TarifID";
                        SqlCommand deleteIngredientsCommand = new SqlCommand(deleteIngredientsQuery, connection);
                        deleteIngredientsCommand.Parameters.AddWithValue("@TarifID", TarifID);
                        deleteIngredientsCommand.ExecuteNonQuery();

                        // Tarifi Tarifler tablosundan sil
                        string deleteTarifQuery = "DELETE FROM Tarifler WHERE TarifID = @TarifID";
                        SqlCommand deleteTarifCommand = new SqlCommand(deleteTarifQuery, connection);
                        deleteTarifCommand.Parameters.AddWithValue("@TarifID", TarifID);
                        deleteTarifCommand.ExecuteNonQuery();

                        MessageBox.Show("Tarif başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();

                        // Mevcut Form1 üzerindeki DataGridView'i yeniden doldur
                        AnaSayfa form1 = (AnaSayfa)Application.OpenForms["Form1"];
                        if (form1 != null)
                        {
                            
                            List<string> tarifAdlari = GetTarifAdlari();
                            TarifListeleme tarifListeleme = new TarifListeleme(connectionString);
                            tarifListeleme.TarifleriGoster(tarifAdlari, form1.GetDataGridView());
                        }



                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            form1.FormdaTarifleriGosterme();

        }

        private void tarifigüncellebutton_Click(object sender, EventArgs e)
        { 

            TarifGuncellemeForm guncelleForm = new TarifGuncellemeForm(TarifID, TarifAdi, Kategori, HazirlamaSuresi, Talimatlar, form1);
            guncelleForm.ShowDialog();
            this.Close();
            form1.FormdaTarifleriGosterme();
        }




    }
}
