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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DenizliValiliğiYemekRezervasyonu
{
    public partial class UserControlDays : UserControl
    {
        private Form activeForm,form;
        public string tc;
        public bool IsPastDate { get; set; }
        public Label lbl_userCount;
        public static string static_day,staticday;
        public UserControlDays(string metin)
        {
            InitializeComponent();
            this.Click += new EventHandler(UserControlDays_Click); // Click olayı ekleme
            this.DoubleClick += new EventHandler(UserControlDays_DoubleClick);
            label2.Text = metin;
            lbl_userCount = new Label(); // Yeni Label ekleniyor
            
            lbl_userCount.AutoSize = true;
            lbl_userCount.Location = new Point(5, 5); // Label'ın pozisyonu
            this.Controls.Add(lbl_userCount);
            
            Repository repository = new Repository();
            repository.giveDateofYemekListesi(secilmis);
            repository.giveDate(selectedDate,metin);

        }
        List<DateTime> secilmis = new List<DateTime>();//yemek için
        List<DateTime> selectedDate = new List<DateTime>(); //daha önce yemek kaydı yapmış kişiler için


        public void days(int numday, bool isPastDate)
        {
            lbl_days.Text = numday.ToString();
            this.Tag = numday;
            
            IsPastDate = isPastDate;

            if (IsPastDate)
            {
                this.BackColor = Color.LightGray; // Geçmiş tarihler için gri renk
                this.Enabled = false; // Geçmiş tarihler tıklanamaz
            }
            else
            {
                this.BackColor = Color.White; // Geçmiş olmayan tarihler için beyaz renk
                this.Enabled = true; // Geçmiş tarihler tıklanabilir
            }
            
        }
        private void UserControlDays_Click(object sender, EventArgs e)
        {
            int day = int.Parse(lbl_days.Text);
            int month = int.Parse(Anasayfa.smonth);
            int year = int.Parse(Anasayfa.syear);
            DateTime date = new DateTime(year, month, day);
            if (!selectedDate.Contains(date))//daha önce yemek kaydı yoksa
            {
                if (secilmis.Contains(date))//menüye yemek eklenmişse
                {
                    if (this.BackColor != Color.SlateGray)
                    {
                        this.BackColor = Color.Green; // Kutucuğun rengini değiştir
                        static_day = lbl_days.Text;
                        staticday = lbl_days.Text;
                    }
                    else
                    {
                        this.BackColor = Color.SlateGray;
                    }
                }
                else
                {
                    // Eğer tarih secilmis listesinde yoksa tıklama işlemini iptal edebilirsin
                    MessageBox.Show("Bu tarih için herhangi bir yemek kaydı mevcut değil.");
                    return;
                }
            }
            else
            {
                return;
            }
            
        }

        private void UserControlDays_DoubleClick(object sender, EventArgs e)
        {
            this.BackColor= Color.White;    
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if(form == null || form.IsDisposed)
            {
                if (string.Equals(label2.Text, "11111111111"))
                {
                    int day = int.Parse(staticday);
                    int month = int.Parse(Anasayfa.smonth);
                    int year = int.Parse(Anasayfa.syear);
                    DateTime date = new DateTime(year, month, day);
                    if (!secilmis.Contains(date))
                    {
                        form = new YemekKayıt();
                        form.Show();
                    }
                }
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            staticday = lbl_days.Text;
            if (activeForm == null || activeForm.IsDisposed)
            {
                activeForm = new Yemek();
                activeForm.Show();
            }
            else
            {
                activeForm.Show();
            }
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            if (activeForm is Yemek && activeForm != null && !activeForm.IsDisposed)
            {
                activeForm.Hide();
            }
        }
    }
}
