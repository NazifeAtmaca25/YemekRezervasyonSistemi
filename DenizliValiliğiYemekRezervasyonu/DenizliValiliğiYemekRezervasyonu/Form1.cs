using DenizliValiliğiYemekRezervasyonu.controller;
using DenizliValiliğiYemekRezervasyonu.enumacation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenizliValiliğiYemekRezervasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_giris_Click(object sender, EventArgs e)
        {
            Controller controller=new Controller();
            LoginState result = controller.login(txt_name.Text, txt_pwd.Text);

            if (LoginState.successful == result)
            {
                Anasayfa anasayfa = new Anasayfa(txt_name.Text);
                anasayfa.Show();
                this.Hide();
            }
            else if (LoginState.failed == result) { 
                MessageBox.Show("Hatalı giriş","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Lütfen kutucukları doldurunuz","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            registerPage.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgetPassword forgetPassword = new ForgetPassword();
            forgetPassword.Show();
            this.Hide();
        }
    }
}
