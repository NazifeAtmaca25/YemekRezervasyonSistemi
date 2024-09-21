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
    public partial class Yemek : Form
    {
        
        public Yemek()
        {
            InitializeComponent();
            
        }

        private void Yemek_Load(object sender, EventArgs e)
        {
            Repository repository = new Repository();
            int day = int.Parse(UserControlDays.staticday);
            int month = int.Parse(Anasayfa.smonth);
            int year = int.Parse(Anasayfa.syear);
            DateTime date = new DateTime(year, month, day);

            SqlConnection conn = new SqlConnection("Server=localhost;Initial Catalog=yemekRezervasyon ;User ID=sa;password=12345");
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT birinci, ikinci, ücüncü, dördüncü FROM yemekListesi WHERE dates=@dates", conn);
                cmd.Parameters.AddWithValue("@dates", date);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    LoadYemekData(reader["birinci"].ToString(), reader["ikinci"].ToString(), reader["ücüncü"].ToString(), reader["dördüncü"].ToString());
                }
            }
        }
        public void LoadYemekData(string yemek1, string yemek2, string yemek3, string yemek4)
        {
            lbl_bir.Text = yemek1;
            lbl_iki.Text = yemek2;  
            lbl_üc.Text = yemek3;
            lbl_dort.Text = yemek4;
        }
    }
}
