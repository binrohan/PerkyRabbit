using System.IO;
using System;
using System.Threading.Tasks;
using Dtos;
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

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        
        public async Task SendAsync(MailToSendDto mail)
        {
            var email = new MimeMessage();
            
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);

            email.To.Add(MailboxAddress.Parse(mail.To));
            email.Cc.Add(MailboxAddress.Parse(mail.CC));
            email.Bcc.Add(MailboxAddress.Parse(mail.BCC));
            
            email.Subject = mail.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mail.Body.ToString();
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            try
            {
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                smtp.Disconnect(true);
            }
        }
    }
}
