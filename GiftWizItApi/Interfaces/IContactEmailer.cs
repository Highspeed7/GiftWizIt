using GiftWizItApi.EmailTemplateModels;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IContactEmailer
    {
        Task SendEmailTransactionalAsync(string email, ContactMailTemplate templateData);
        Task SendEmailAsync(string email, string subject, string message);
    }
}
