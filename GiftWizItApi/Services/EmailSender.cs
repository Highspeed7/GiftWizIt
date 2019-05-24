using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace GiftWizItApi.Service
{
    public class EmailSender : IEmailSender
    {
        public EmailOptions Options { get; set; }

        public EmailSender(IOptions<EmailOptions> emailOptions)
        {
            Options = emailOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.ApiKey, subject, message, email);
        }

        protected Task Execute<T>(string email, T templateData, string templateId)
        {
            var client = new SendGridClient(Options.ApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.FromEmail, Options.EmailTitle),

            };

            msg.AddTo(new EmailAddress(email));
            msg.SetTemplateData(templateData);
            msg.SetTemplateId(templateId);

            try
            {
                return client.SendEmailAsync(msg);
            }catch (Exception ex)
            {
                // TODO: Logging
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        protected Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.FromEmail, Options.EmailTitle),
                Subject = subject,
                HtmlContent = message,
                ReplyTo = new EmailAddress(Options.FromEmail, Options.EmailTitle)
            };

            msg.AddTo(new EmailAddress(email));

            try
            {
                return client.SendEmailAsync(msg);
            }catch (Exception ex)
            {
                // TODO: Logging
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
