using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace PackMarket.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Execute(email, subject, htmlMessage);
        }
        public async Task Execute(string email,string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация сайта russ-pack.ru", "archey.net@ya.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using(var client=new SmtpClient())
            {
                client.Connect("smtp.yandex.ru", 465, true);
                client.Authenticate("archey.net", "Vedanieya2023.");
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
