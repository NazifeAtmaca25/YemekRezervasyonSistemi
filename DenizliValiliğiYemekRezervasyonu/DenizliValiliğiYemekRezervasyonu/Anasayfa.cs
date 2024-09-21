using DenizliValiliğiYemekRezervasyonu.controller;
using DenizliValiliğiYemekRezervasyonu.dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenizliValiliğiYemekRezervasyonu
{
    public partial class Anasayfa : Form
    {
        int month, year;
        int count = 0;
        string emailbyTC, tcAnasayfa;
        public static string smonth,syear;
        public static DateTime currentDate;
        Repository repository = new Repository();
        List<DateTime> selectedDates = new List<DateTime>();//listboxa eklene tarih
        List<DateTime> selectedDate = new List<DateTime>();//kullanıcının daha önce eklediği tarihler
        public Anasayfa(string metin)
        {
            InitializeComponent();
       
            repository.giveDate(selectedDate,metin);  
            label1.Text = repository.GetUserNameByTC(metin)+" Hoşgeldiniz";
            emailbyTC=repository.getEmailbyTc(metin);
            tcAnasayfa = metin;
            UserControlDays userControlDays = new UserControlDays(tcAnasayfa);
            lbl_price.Text = "Güncel Yemek Fiyatı: "+repository.lastPrice().ToString()+"TL";

        }
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            displays();
            groupBox2.Visible = false;
            if (tcAnasayfa== "11111111111")
            {
                groupBox4.Visible = true;
            }
            else
            {
                groupBox4.Visible = false;
            }
        }


        private void displays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Text = monthName + " " + year;

            smonth = month.ToString();
            syear = year.ToString();

            DateTime startofMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysoftheweek = ((int)startofMonth.DayOfWeek + 6) % 7;
            
            for (int i = 1; i <= daysoftheweek; i++)//kutulara usercontrolleri burada ekliyor
            {
                UserControlBlank userControlBlank = new UserControlBlank();
                daycontainer.Controls.Add(userControlBlank);
            }

            for (int i = 1; i <= days; i++)//labellara tarihlerin sayılarını giriyor
            {
                bool isPastDate = new DateTime(year, month, i) < now;
                UserControlDays userControlDays = new UserControlDays(tcAnasayfa);
                userControlDays.days(i, isPastDate);

                if (i == DateTime.Now.Day && month == DateTime.Now.Month && year == DateTime.Now.Year)
                {
                    userControlDays.BackColor = Color.Gray;
                }
                currentDate = new DateTime(year, month, i);
                
                if (tcAnasayfa == "11111111111")//burada o günkü toplam yemek yicek kişileri belirliyoruz
                {
                    int count = repository.countofUser(currentDate);
                    userControlDays.lbl_userCount.Text = count.ToString();
                }

                daycontainer.Controls.Add(userControlDays);
            }

            foreach (UserControlDays control in daycontainer.Controls.OfType<UserControlDays>())
            {
                int day = (int)control.Tag;
                DateTime date = new DateTime(int.Parse(syear), int.Parse(smonth), day);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {//hafta sonları menü yazısının çıkmasını engelliyor
                    control.label1.Visible = false;
                }
                
               
                if (selectedDate.Contains(date))
                {
                    if (day == DateTime.Now.Day)
                    {
                        control.BackColor = Color.Gray;
                    }
                    else
                    { // Kullanıcının daha önce seçtiği tarihleri maviye boyar
                        control.BackColor = Color.DeepSkyBlue;
                    } 
                }  

            }
           
        }

        private void btn_onceki_Click(object sender, EventArgs e)
        {
            if (month == DateTime.Now.Month && year == DateTime.Now.Year)
            {
                return;
            }

            daycontainer.Controls.Clear();
            month--;

            if (month < 1)
            {
                month = 12;
                year--;
            }
            smonth = month.ToString();
            syear = year.ToString();
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Text = monthName + " " + year;
            DateTime startofMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysoftheweek = ((int)startofMonth.DayOfWeek + 6) % 7;

            for (int i = 1; i <= daysoftheweek; i++)
            {
                UserControlBlank userControlBlank = new UserControlBlank();
                daycontainer.Controls.Add(userControlBlank);
            }

            for (int i = 1; i <= days; i++)
            {
                bool isPastDate = new DateTime(year, month, i) < DateTime.Now;
                UserControlDays userControlDays = new UserControlDays(tcAnasayfa);
                userControlDays.days(i, isPastDate);
                if (i == DateTime.Now.Day && month == DateTime.Now.Month && year == DateTime.Now.Year)
                {
                    userControlDays.BackColor = Color.Gray;
                }
                currentDate = new DateTime(year, month, i);
                
                if (tcAnasayfa == "11111111111")
                {
                    int count = repository.countofUser(currentDate);
                    userControlDays.lbl_userCount.Text = count.ToString();
                }

                daycontainer.Controls.Add(userControlDays);
            }
            foreach (UserControlDays control in daycontainer.Controls.OfType<UserControlDays>())
            {
                int day = (int)control.Tag;
                DateTime date = new DateTime(int.Parse(syear), int.Parse(smonth), day);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    control.label1.Visible = false;
                }
                if (selectedDates.Contains(date))//bu ekleme yaptıktan sonra sayfa değiştirisek renklerin aynı kalması için
                {
                    if (control.BackColor == Color.SlateGray)
                    {
                        control.BackColor = Color.SlateGray;
                    }
                    else
                    {
                        control.BackColor = Color.Green;
                    }
                }
                if (selectedDate.Contains(date))
                {
                    if (day == DateTime.Now.Day)
                    {
                        control.BackColor = Color.Gray;
                    }
                    else
                    {
                        control.BackColor = Color.DeepSkyBlue;
                    }
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)//silme tuşu
        {
            int day = int.Parse(UserControlDays.static_day);
            int month = int.Parse(smonth);
            int year = int.Parse(syear);
            DateTime date = new DateTime(year, month, day);
            if (selectedDates.Contains(date))//listboxdayda ve o tarihi silme işlemi
            {
                selectedDates.Remove(date);
                listBox1.Items.Remove(date.ToString("dd.MM.yyyy"));
                count--;
                lbl_odenecektutar.Text = Convert.ToString(count * repository.lastPrice())+"TL";
                foreach (UserControlDays control in daycontainer.Controls.OfType<UserControlDays>())
                {
                    if ((int)control.Tag == day) // Tag özelliğini kullanarak karşılaştır
                    {
                        control.BackColor = Color.White;
                        break;
                    }
                }
                
            }

            }
        
        Random random = new Random();
        string onaykodu;
        private void btn_odeme_Click(object sender, EventArgs e)//odaykodu gönderme tuşu
        {
            Controller controller = new Controller();
            onaykodu = random.Next(10000, 100000).ToString();
            controller.sendEmail(emailbyTC, onaykodu);
            MessageBox.Show("Onay kodu mailinize gönderilmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            groupBox2.Visible = true;
        }
   
        private void btn_onayla_Click(object sender, EventArgs e)
        {
         
            if (txt_onayla.Text == onaykodu)
            {
                foreach(DateTime date in selectedDates)
                {
                    repository.addRecord(tcAnasayfa, date);
                }
                lbl_odenecektutar.Text = "0";
                foreach (UserControlDays control in daycontainer.Controls.OfType<UserControlDays>())
                {//yaptığımız yeni kayıtları koyu griye boyar
                    int day = (int)control.Tag;
                    DateTime date = new DateTime(int.Parse(syear), int.Parse(smonth), day);
                    if (selectedDates.Contains(date))
                    {
                        control.BackColor = Color.SlateGray;
                    }
                }
                listBox1.Items.Clear();
                MessageBox.Show("Rezervasyonunuz onaylanmıştır", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupBox2.Visible= false;
            } 
            else
            {
                MessageBox.Show("Onay kodu yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)//ÇIKIŞ butonu
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void btn_price_Click(object sender, EventArgs e)
        {
            if (txt_price.Text != null)
            {
                repository.addPrice(txt_price.Text);
            }
           
        }

        private void btn_kayıt_Click(object sender, EventArgs e)//EKLE TUŞU
        {
            
            int day = int.Parse(UserControlDays.static_day);
            int month = int.Parse(smonth);
            int year = int.Parse(syear);
            DateTime date = new DateTime(year, month, day);
            if (!selectedDates.Contains(date))
            {
                selectedDates.Add(date);
                listBox1.Items.Add(date.ToString("dd.MM.yyyy"));
                count++;
                lbl_odenecektutar.Text = Convert.ToString(count * repository.lastPrice())+"TL";
            }
            else
            {
                MessageBox.Show("Bu tarih zaten seçilmiş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_sonraki_Click(object sender, EventArgs e)
        {
            if (month == DateTime.Now.Month + 1 && year == DateTime.Now.Year)
            {
                return; 
            }

            daycontainer.Controls.Clear();
            month++;

            if (month > 12)
            {
                month = 1;
                year++;
            }
            smonth = month.ToString();
            syear = year.ToString();
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbDate.Text = monthName + " " + year;
            DateTime startofMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int daysoftheweek = ((int)startofMonth.DayOfWeek + 6) % 7;

            for (int i = 1; i <= daysoftheweek; i++)
            {
                UserControlBlank userControlBlank = new UserControlBlank();
                daycontainer.Controls.Add(userControlBlank);
            }

            for (int i = 1; i <= days; i++)
            {
                bool isPastDate = new DateTime(year, month, i) < DateTime.Now;
                UserControlDays userControlDays = new UserControlDays(tcAnasayfa);
                userControlDays.days(i, isPastDate);
                if (i == DateTime.Now.Day && month == DateTime.Now.Month && year == DateTime.Now.Year)
                {
                    userControlDays.BackColor = Color.Gray;
                }
                currentDate = new DateTime(year, month, i);
               
                if (tcAnasayfa == "11111111111")
                {
                    int count = repository.countofUser(currentDate);
                    userControlDays.lbl_userCount.Text = count.ToString();
                }

                daycontainer.Controls.Add(userControlDays);
            
            }
            foreach (UserControlDays control in daycontainer.Controls.OfType<UserControlDays>())
            {
                int day = (int)control.Tag;
                DateTime date = new DateTime(int.Parse(syear), int.Parse(smonth), day);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    control.label1.Visible = false;
                }
                if (selectedDates.Contains(date))
                {
                    if (control.BackColor == Color.SlateGray)
                    {
                        control.BackColor = Color.SlateGray;
                    }
                    else
                    {
                        control.BackColor = Color.Green;
                    }
                }
                if (selectedDate.Contains(date))
                {
                    if (day == DateTime.Now.Day)
                    {
                        control.BackColor = Color.Gray;
                    }
                    else
                    {
                        control.BackColor = Color.DeepSkyBlue;
                    }
                }
            }
            
            

        }
     
    }
}
