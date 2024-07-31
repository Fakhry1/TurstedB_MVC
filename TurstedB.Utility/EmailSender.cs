using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TrustedB.Utility
{
    public class EmailSender : IEmailSender
    {
        //public string SendGridKey { get; set; }
        //public EmailSender(IConfiguration _config)
        //{
        //    SendGridKey = _config.GetValue<string>("SendGrid:SecretKey");
        //}

        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    var client = new SendGridClient(SendGridKey);
        //    var from_email = new EmailAddress("hello@dotnetmastery.com", "DotNetMastery - Identity Manager");

        //    var to_email = new EmailAddress(email);

        //    var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, "", htmlMessage);
        //    return client.SendEmailAsync(msg);
        //}


        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.office365.com",587) { 
            
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("Fakhreldeen.satti2024@outlook.com","ControlControl123@")

            };

            return client.SendMailAsync(new MailMessage(from: "Fakhreldeen.satti2024@outlook.com",
                to: email, subject, htmlMessage));
        }
    }
}
