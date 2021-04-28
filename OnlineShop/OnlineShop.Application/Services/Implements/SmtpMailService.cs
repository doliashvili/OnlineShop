using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineShop.Application.Services.Abstract;
using OnlineShop.Application.Settings;
using OnlineShop.Domain.CommonModels.Mail;

namespace OnlineShop.Application.Services.Implements
{
    public class SmtpMailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<SmtpMailService> _logger;

        public SmtpMailService(IOptions<MailSettings> mailSettings, ILogger<SmtpMailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(request.From ?? _mailSettings.From),
                    Subject = request.Subject,
                    IsBodyHtml = true,
                    Body = request.Body,
                };
                mailMessage.To.Add(new MailAddress(request.To));

                using var smtp = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true //TODO maybe false in production and change port
                };

                await smtp.SendMailAsync(mailMessage);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex); //TODO: Remove try catch and implement resiliency with polly
                Console.WriteLine("errorMail");
            }
        }
    }
}