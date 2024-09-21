using DenizliValiliğiYemekRezervasyonu.dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenizliValiliğiYemekRezervasyonu
{
    public partial class YemekKayıt : Form
    {
        public YemekKayıt()
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection("Server=localhost;Initial Catalog=yemekRezervasyon ;User ID=sa;password=12345");
            using (conn)
            {
                
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT dates FROM yemekListesi", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime tarih = Convert.ToDateTime(reader["dates"]);
                    secilmis.Add(tarih);
                }

                conn.Close();

            }
        }
        List<DateTime> secilmis = new List<DateTime>();
        private void btn_ekle_Click(object sender, EventArgs e)
        {
            Repository repository = new Repository();
            int day = int.Parse(UserControlDays.staticday);
            int month = int.Parse(Anasayfa.smonth);
            int year = int.Parse(Anasayfa.syear);
            DateTime date = new DateTime(year, month, day);
            repository.addFood(txt_bir.Text,txt_iki.Text,txt_üç.Text,txt_dört.Text,date);
        }

        private void YemekKayıt_Load(object sender, EventArgs e)
        {

            txt_dates.Text = UserControlDays.staticday + "." + Anasayfa.smonth + "." + Anasayfa.syear;
        }

        
    }
}
