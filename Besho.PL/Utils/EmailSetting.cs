using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Besho.PL.Utils
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlmessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mohabish224@gmail.com", "htjj lyyw dldo dtsr")
            };

            return client.SendMailAsync(
                new MailMessage(from: "mohabish224@gmail.com",
                                to: email,
                                subject,
                                htmlmessage
                                )
                { IsBodyHtml=true}
                
                );
        }
    }
}
