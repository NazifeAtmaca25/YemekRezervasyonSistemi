using DenizliValiliğiYemekRezervasyonu.controller;
using DenizliValiliğiYemekRezervasyonu.enumacation;
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
    public partial class RegisterPage : Form
    {
        SqlConnection con=new SqlConnection("Server=localhost;Initial Catalog=yemekRezervasyon ;User ID=sa;password=12345");
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_userId.Text) && !string.IsNullOrEmpty(txt_tc.Text) && !string.IsNullOrEmpty(txt_pwd.Text) && !string.IsNullOrEmpty(txt_email.Text))
            {
                Controller controller = new Controller();
                LoginState result = controller.isSame(txt_tc.Text);
                if (result == LoginState.successful)
                {
                    int result2 = addRecord();
                    if (result2 == 1)
                    {
                        MessageBox.Show("Kaydınız gerçekleşmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form1 form = new Form1();
                        form.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kaydnız gerçekleşmemiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Bu kimlikte kayıtlı bir kullanıcı bulunmaktadır.","Bilgilendirme",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            else
            {
                MessageBox.Show("Lütfen bol alanları doldurunuz!","Bilgilendirme",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txt_userId_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }
        public int addRecord()
        {
            
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into users (userId,tc,email,pwd) values (@userId, @tc, @email, @pwd)",con);
            cmd.Parameters.AddWithValue("@userId",txt_userId.Text);
            cmd.Parameters.AddWithValue("@tc", txt_tc.Text);
            cmd.Parameters.AddWithValue("@email",txt_email.Text);
            cmd.Parameters.AddWithValue("@pwd",txt_pwd.Text);
            int returnValue = cmd.ExecuteNonQuery();
            con.Close();
            if (returnValue == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
