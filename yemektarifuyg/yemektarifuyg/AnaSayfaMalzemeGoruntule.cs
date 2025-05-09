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
    public partial class AnaSayfaMalzemeGoruntule : Form
    {
        public AnaSayfaMalzemeGoruntule()
        {
            InitializeComponent();
            MalzemeVeTariflerileriGoruntule(); 
        }

        private void MalzemeVeTariflerileriGoruntule()
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";

            string query = @"
                   SELECT 
                    m.MalzemeAdi, 
                    m.MalzemeBirim, 
                    m.BirimFiyat, 
                    m.ToplamMiktar, 
                    t.TarifAdi,
                    tm.MalzemeMiktar
                    FROM Malzemeler m
                    LEFT JOIN TarifMalzemeIliskisi tm ON m.MalzemeID = tm.MalzemeID
                    LEFT JOIN Tarifler t ON t.TarifID = tm.TarifID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Sütun başlıklarını ve genişliklerini ayarla
                dataGridView1.Columns["MalzemeAdi"].HeaderText = "Malzeme Adı";
                dataGridView1.Columns["MalzemeBirim"].HeaderText = "Malzeme Birimi";
                dataGridView1.Columns["BirimFiyat"].HeaderText = "Birim Fiyat";
                dataGridView1.Columns["ToplamMiktar"].HeaderText = "Toplam Miktar";
                dataGridView1.Columns["TarifAdi"].HeaderText = "Tarif Adı";
                dataGridView1.Columns["MalzemeMiktar"].HeaderText = "Malzeme Miktarı";

                // Sütun genişliklerini ayarla (sabit olarak belirlenmiş genişlikler)
                dataGridView1.Columns["MalzemeAdi"].Width = 175;
                dataGridView1.Columns["MalzemeBirim"].Width = 125;
                dataGridView1.Columns["BirimFiyat"].Width = 125;
                dataGridView1.Columns["ToplamMiktar"].Width = 125;
                dataGridView1.Columns["TarifAdi"].Width = 200;
                dataGridView1.Columns["MalzemeMiktar"].Width = 125;
            }
        }

    }


}