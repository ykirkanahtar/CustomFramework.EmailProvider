using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CustomFramework.EmailProvider
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly IEmailConfig _emailConfig;

        public EmailSender(ILogger<EmailSender> logger, IEmailConfig emailConfig)
        {
            _logger = logger;
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(string from, IList<string> toList, string subject, string message, bool isBodyHtml)
        {
            try
            {
                var emailMessage = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isBodyHtml
                };

                foreach (var to in toList)
                {
                    emailMessage.To.Add(to);
                }

                var client = new SmtpClient
                {
                    Host = _emailConfig.MailServer,
                    Port = _emailConfig.MailServerPort,
                    EnableSsl = _emailConfig.EnableSsl,
                    UseDefaultCredentials = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password)
                };

                await client.SendMailAsync(emailMessage);
            }
            catch (Exception e)
            {
                _logger.LogCritical(0, e, e.Message);
                throw;
            }
        }

        public async Task SendEmailAsync(string from, string to, string subject, string message, bool isBodyHtml)
        {
            var toList = new List<string>();
            toList.Add(to);
            await SendEmailAsync(from, toList, subject, message, isBodyHtml);
        }
    }
}