using GiftWizItApi.EmailTemplateModels;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Service;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GiftWizItApi.Services
{
    public class ContactEmailer : EmailSender, IContactEmailer
    {
        private readonly IOptions<EmailOptions> emailOptions;

        public ContactEmailer(IOptions<EmailOptions> emailOptions) : base(emailOptions)
        {
            this.emailOptions = emailOptions;
        }

        public Task SendEmailTransactionalAsync(string email, ContactMailTemplate templateData)
        {
            return base.Execute<ContactMailTemplate>(email, templateData, emailOptions.Value.ContactGreetTemplateId);
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await SendEmailAsync(email, subject, message);
        }
    }
}
