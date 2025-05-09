using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace yemektarifuyg
{
    internal class VeriTabaniBaglantisi
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=TarifDefteriUygulamasi;Integrated Security=True;");

        public static void BaglantiKontrolu()
        {

            if (baglanti.State == System.Data.ConnectionState.Closed)
            {
                baglanti.Open();
            }

            else { }
        }



    }
}
