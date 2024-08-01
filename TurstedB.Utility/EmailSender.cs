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
using System;
using System.Collections.Generic;
using Azure;
using Azure.Communication.Email;



namespace TrustedB.Utility
{
    public class EmailSender : IEmailSender
    {
        const string connectionString = "endpoint=https://truest-communication.unitedstates.communication.azure.com/;accesskey=5RaeiQc5J2e4kli3Z8emCGNVyuGvu9blCXR8h6hi1oqxmLmSVxwPJQQJ99AHACULyCpAIX8yAAAAAZCSXAd5";
        EmailClient emailClient = new EmailClient(connectionString);


       


        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            EmailSendOperation msg = emailClient.Send(
            WaitUntil.Completed,
            senderAddress: "DoNotReply@594485c5-c4b7-4ed6-9abe-b938b6558f8e.azurecomm.net",
            recipientAddress: email,
            subject: subject,
            htmlContent: "<html><h1>change password vi email.</h1l></html>",
            plainTextContent: htmlMessage);

            //var client = new SendGridClient(SendEmail);
            // var from_email = new EmailAddress("DoNotReply@594485c5-c4b7-4ed6-9abe-b938b6558f8e.azurecomm.net", "DoNotReply");

            // var to_email = new EmailAddress(email);
            // emailSendOperation.se
            // var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, "", htmlMessage);
            return null;

        }



        //string emailConnection = Environment.GetEnvironmentVariable("endpoint=https://truest-communication.unitedstates.communication.azure.com/;accesskey=5RaeiQc5J2e4kli3Z8emCGNVyuGvu9blCXR8h6hi1oqxmLmSVxwPJQQJ99AHACULyCpAIX8yAAAAAZCSXAd5");
        //var emailClient = new EmailClient(emailConnection);


        //public string SendEmail { get; set; }
        //public EmailSender(IConfiguration _config)
        //{
        //   SendEmail = _config.GetValue<string>("SendEmail:EmailConnection");
        //}

        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    var client = new SendGridClient(SendEmail);
        //    var from_email = new EmailAddress("DoNotReply@594485c5-c4b7-4ed6-9abe-b938b6558f8e.azurecomm.net", "DoNotReply");

        //    var to_email = new EmailAddress(email);

        //    var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, "", htmlMessage);
        //    return client.SendEmailAsync(msg);
        //}


        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    var client = new SmtpClient("smtp.gmail.com", 587) { 

        //        EnableSsl = true,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential("fakhreldeensatti29@gmail.com", "Control@123")

        //    };

        //    return client.SendMailAsync(new MailMessage(from: "fakhreldeensatti29@gmail.com",
        //        to: email, subject, htmlMessage));
        //}
    }
}
