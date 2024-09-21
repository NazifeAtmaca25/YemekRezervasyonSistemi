using DenizliValiliğiYemekRezervasyonu.dao;
using DenizliValiliğiYemekRezervasyonu.enumacation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DenizliValiliğiYemekRezervasyonu.controller
{
    public class Controller
    {
        Repository repository;

        public Controller()
        {
            repository = new Repository();
        }

        public LoginState login(string tc,string pwd)
        {
            if (!string.IsNullOrEmpty(tc) && !string.IsNullOrEmpty(pwd))
            {
                LoginState result = repository.login(tc, pwd);
                return result;
            }
            else
            {
                return LoginState.incompleteParameter;
            }
        }

        public LoginState isSame(string tc)
        {
            int result = repository.isSame(tc);
            if (result == 0)
            {
                return LoginState.successful;
            }
            else
            {
                return LoginState.failed;
            }
        }

        public LoginState changePassword(string tc,string email)
        {
            int result=repository.changePassword(tc,email);
            if (result == 1)
            {
                return LoginState.successful;
            }
            else
            {
                return LoginState.failed;
            }
        }

        public LoginState takePassword(string tc, string pwd)
        {
            LoginState result=repository.takePassword(tc,pwd);
            if (result == LoginState.successful)
            {
                return LoginState.successful;
            }
            else
            {
                return LoginState.failed;
            }

        }

        public void sendEmail(string email, string onaykodu)
        {

            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("yemekrezervasyon1907@gmail.com", "kycs emdf teqp rimc"),
            };
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("yemekrezervasyon1907@gmail.com"),
                Subject = "Onay kodu",
                Body = "Onay kodunuz:" + onaykodu,
            };

            mail.To.Add(email);
            smtp.Send(mail);

        }
    }
}
