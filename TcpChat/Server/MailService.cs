using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MailService
    {
        SmtpClient smtpClient;
        public string ConfirmToken = "";
        Random r = new Random();

        public MailService()
        {
            smtpClient = new SmtpClient("aspmx.l.google.com",25);
        }

        public string GenerateToken()
        {
            for(int i=0;i<5;i++)
            {
                ConfirmToken += Convert.ToChar(r.Next('A', 'Z'));
            }
            return ConfirmToken;
        }

        public void SendConfirmEmail(string Email)
        {
            MailMessage mail = new MailMessage("vasyan@gmail.com", Email);
            mail.Subject = "Confirm Email";
            mail.Body = $"<h1>{ConfirmToken}</h1>";
            mail.IsBodyHtml = true;
            smtpClient.Send(mail);
        }

    }
}
