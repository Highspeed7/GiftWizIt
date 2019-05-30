using GiftWizItApi.Models;

namespace GiftWizItApi.EmailTemplateModels
{
    public class ContactGreetMailTemplate
    {
        public EmailAddress contactEmail { get; set; }
        public string fromUser { get; set; }
        public string getStartedUrl { get; set; }
    }
}
