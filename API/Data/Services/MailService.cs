using System.IO;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;
using Data.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Data.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IConfiguration _config;

        public MailService(IOptions<MailSettings> mailSettings, IConfiguration config)
        {
            _config = config;
            _mailSettings = mailSettings.Value;
        }
        public async Task SendAsync(MailToSend mail)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.Subject = mail.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mail.Body;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
