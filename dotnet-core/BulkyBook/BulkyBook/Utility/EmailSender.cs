using System;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        public string sendGridSecret { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            sendGridSecret = configuration.GetValue<string>("SendGrid:Secret");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("thanh.vo@monstar-lab.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            // send email
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("thanh.vo@monstar-lab.com", "gcmecftfwpwomaae");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;

            //var client = new SendGridClient(sendGridSecret);
            //var from = new EmailAddress("thanhne@jthanh8144.studio", "Bulky Book");
            //var to = new EmailAddress(email);
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            //return client.SendEmailAsync(msg);
        }
    }
}
