using DenizliValiliğiYemekRezervasyonu.controller;
using DenizliValiliğiYemekRezervasyonu.enumacation;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace DenizliValiliğiYemekRezervasyonu
{
    public partial class ForgetPassword : Form
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }

        Random random = new Random();
        string onaykodu;
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_tcsifre.Text) && !string.IsNullOrEmpty(txt_emailsifre.Text))
            {
                Controller controller = new Controller();
                LoginState result = controller.changePassword(txt_tcsifre.Text, txt_emailsifre.Text);

                if (result == LoginState.successful)
                {
                    onaykodu=random.Next(10000,100000).ToString();
                    controller.sendEmail(txt_emailsifre.Text,onaykodu);
                    MessageBox.Show("Onay kodu mailinize gönderilmiştir.", "Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    groupBox1.Visible = true;
                }
                else
                {
                    MessageBox.Show("Bu kayıtta bir kullanıcı bulunmamakta lütfen tc ve emailinizi kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_onaykodu.Text == onaykodu)
            {
                string yazı = txt_tcsifre.Text;
                NewPassword newPassword = new NewPassword(yazı);
                newPassword.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Onay kodu yanlış!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void ForgetPassword_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

       
        private void button3_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
