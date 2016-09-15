using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Mobiles.Models
{
    public class EmailSender
    {
        public static void Send(string toName, string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    Body = body,
                    Subject = subject,
                    IsBodyHtml = true,

                };
                mailMessage.To.Add(new MailAddress(toEmail, toName));
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
            }
            catch
            {
                
            }
        }
    }
}