using DenizliValiliğiYemekRezervasyonu.controller;
using DenizliValiliğiYemekRezervasyonu.dao;
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
    public partial class NewPassword : Form
    {
        public NewPassword(string gelenMetin)
        {
            InitializeComponent();
            textBox1.Text = gelenMetin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            LoginState result = controller.takePassword(textBox1.Text, txt_yenisifre.Text);
            if (result == LoginState.successful)
            {
                MessageBox.Show("Yeni şifreniz kaydolmultur.","Bilgilendirme",MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hata","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void NewPassword_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        
    }
}
