using AgnosticAlbatros.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static AgnosticAlbatros.Constants;

namespace AgnosticAlbatros.Helpers
{
    public static class MailHelper
    {
        public static void SendRegisterEmail(string userEmailAddress, string password, string fullUserName)
        {
            string subject = "Welkom bij DeliGate!";
            string body = "Hallo " + fullUserName + "."
                            + "<br /><br />"
                            + "Uw registratie bij DeliGate is goed verlopen. <br />"
                            + "U kan inloggen in uw account met onderstaand wachtwoord. <br />"
                            + password
                            + "<br /><br /><br /><br />"
                            + "Met vriendelijke groeten <br />"
                            + "DeliGate";

            SendEmail(userEmailAddress, body, subject);
        }

        private static void SendEmail(string userEmailAddress, string mailBody, string subject)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(MailValues.HostEmail);
                mailMessage.To.Add(userEmailAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                var smtpClient = new SmtpClient();

                smtpClient.EnableSsl = true;
                smtpClient.Port = MailValues.Port;
                smtpClient.Host = MailValues.Host;

                smtpClient.Credentials = new NetworkCredential(MailValues.HostEmail, EncryptionHelper.Decrypt(Encrypted.enc_p1, MailValues.HostEmail));
                smtpClient.Send(mailMessage);
            }

            catch (SmtpException ex)
            {
                Debug.WriteLine("Mail send failed:" + ex.Message);
            }
        }
    }
}
