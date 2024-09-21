using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DenizliValiliğiYemekRezervasyonu.enumacation;

namespace DenizliValiliğiYemekRezervasyonu.dao
{
    public class Repository
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        int returnValue;

        public Repository()
        {
            conn = new SqlConnection("Server=localhost;Initial Catalog=yemekRezervasyon ;User ID=sa;password=12345");
        }

        public LoginState login(string tc,string pwd)
        {
            conn.Open();
            cmd=new SqlCommand("login_sp",conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tc", tc);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            returnValue = (int)cmd.ExecuteScalar();
            conn.Close();
            if (returnValue == 1)
            {
                return LoginState.successful;
            }
            else
            {
                return LoginState.failed;
            }
        }

        public int isSame( string tc) {
        conn.Open();
            cmd=new SqlCommand("register_sq", conn );
            cmd.CommandType=System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tc", tc);
            returnValue = (int)cmd.ExecuteScalar();
            conn.Close();
            return returnValue;
        }

        public int changePassword(string tc,string email)
        {
            conn.Open();
            cmd = new SqlCommand("password_sp", conn);
            cmd.CommandType= System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tc", tc);
            cmd.Parameters.AddWithValue("@email", email);
            returnValue = (int)cmd.ExecuteScalar();
            conn.Close ();
            return returnValue;
        }

        public LoginState takePassword(string tc, string pwd) { 
        
            conn.Open();
            cmd = new SqlCommand("register_sq", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tc", tc);
            returnValue = (int)cmd.ExecuteScalar();
            
            if (returnValue > 0) {
                SqlCommand updateCmd = new SqlCommand("UPDATE users SET pwd = @pwd WHERE tc = @tc", conn);
                updateCmd.Parameters.AddWithValue("@pwd", pwd);
                updateCmd.Parameters.AddWithValue("@tc", tc);
                updateCmd.ExecuteNonQuery();
                conn.Close();
                return LoginState.successful;
            }
            else
            {
                return LoginState.failed;

            }

        }

        public string GetUserNameByTC(string tc)
        {
            string userName = string.Empty;
            conn.Open();
            
            cmd = new SqlCommand("SELECT userId FROM users WHERE tc = @tc", conn);
            cmd.Parameters.AddWithValue("@tc", tc);
            object result = cmd.ExecuteScalar();
              if (result != null)
              {
                userName = result.ToString();
              }
             conn.Close();
            
            return userName;
        }

        public string getEmailbyTc(string tc)
        {
            string email = string.Empty;
            conn.Open();
            cmd = new SqlCommand("Select email From users Where tc=@tc", conn);
            cmd.Parameters.AddWithValue("@tc", tc);
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
              email = result.ToString();
            }
            
            conn.Close();
            return email;
        }

        public void addRecord(string tc, DateTime date)
        {
            conn.Open();
            cmd = new SqlCommand("insert into countofperson(tc , dates) values (@tc , @dates)", conn);
            cmd.Parameters.AddWithValue("@tc",tc);
            cmd.Parameters.AddWithValue ("@dates", date);
            returnValue=cmd.ExecuteNonQuery();
            conn.Close();
            
        }

        public void addPrice(string price)
        {
            conn.Open();
            cmd = new SqlCommand("insert into priceofMeal(price) values (@price)",conn);
            cmd.Parameters.AddWithValue("@price", price);
            returnValue=cmd.ExecuteNonQuery();  
            conn.Close();
            if (returnValue == 1)
            {
                MessageBox.Show("Ekleme işlemi başarılı!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show("Ekleme işlemi başarısız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int lastPrice()
        {
            conn.Open ();
            cmd = new SqlCommand("SELECT TOP 1 price FROM priceofMeal ORDER BY id DESC ", conn);
            object result = cmd.ExecuteScalar();
            int returnValue =Convert.ToInt32(result.ToString());

            conn.Close();
            return returnValue;
        }

        public void addFood(string birinci, string ikinci, string ücüncü,string dördüncü,DateTime date)
        {
            conn.Open();
            cmd = new SqlCommand("insert into yemekListesi(birinci, ikinci, ücüncü, dördüncü, dates) values (@birinci, @ikinci, @ücüncü, @dördüncü, @dates)",conn);
            cmd.Parameters.AddWithValue("@birinci",birinci);
            cmd.Parameters.AddWithValue("@ikinci",ikinci);
            cmd.Parameters.AddWithValue("@ücüncü",ücüncü);
            cmd.Parameters.AddWithValue("@dördüncü",dördüncü);
            cmd.Parameters.AddWithValue("@dates",date);
            returnValue=cmd.ExecuteNonQuery();
            conn.Close();
            if (returnValue == 1)
            {
                MessageBox.Show("Listeye ekleme işlemi başarılı.", "Bilgilendirme");
            }
            else
            {
                MessageBox.Show("Listeye ekleme işlemi başarısız.", "Uyarı");
            }
        }

        public int countofUser(DateTime date)
        {
            conn.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM countofperson WHERE dates = @dates", conn);
            cmd.Parameters.AddWithValue("@dates", date);
            returnValue = (int)cmd.ExecuteScalar();
            conn.Close();
            return returnValue;
        }

        public void giveFood(DateTime date)
        {
            Yemek yemek = new Yemek();
            conn.Open();
            cmd = new SqlCommand("SELECT birinci, ikinci, ücüncü, dördüncü FROM yemekListesi WHERE dates=@dates", conn);
            cmd.Parameters.AddWithValue("@dates", date);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                yemek.LoadYemekData(reader["birinci"].ToString(), reader["ikinci"].ToString(), reader["ücüncü"].ToString(), reader["dördüncü"].ToString());
                yemek.Show();
            }
            conn.Close();
        }

        public void giveDate(List<DateTime> dateList, string metin)
        {
                conn.Open();
                cmd = new SqlCommand("SELECT dates FROM countofperson WHERE tc = @tc", conn);
                cmd.Parameters.AddWithValue("@tc", metin);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dateList.Add(Convert.ToDateTime(reader["dates"]));
                }
                conn.Close();
            
        }

        public void giveDateofYemekListesi(List<DateTime> dateList)
        {
            conn.Open();
            cmd = new SqlCommand("SELECT DISTINCT dates FROM yemekListesi", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime tarih = Convert.ToDateTime(reader["dates"]);
                dateList.Add(tarih);
            }

            conn.Close();
        }


    }

}
