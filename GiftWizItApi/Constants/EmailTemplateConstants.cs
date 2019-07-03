using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Constants
{
    public static class EmailTemplateConstants
    {
        public const string FromName = "GiftWizIt";
        public const string FromAddress = "greetings@giftwizit.com";
        public const string TemplateParentDirectory = "EmailTemplates";

        // Contact Template Constants
        // Greetings
        public const string ContactGreetSubject = "Someone wants to connect!";
        public const string ContactGreetTemplate = "ContactGreetTemplate.html";
        public const string ContactGetStartedDevUrl = "https://localhost:44347/contact-get-started";
        public const string ContactGetStartedProdUrl = "https://www.giftwizit.com/contact-get-started";

        // Sharing
        public const string ContactListShareSubject = "A gift list was shared with you";
        public const string ContactListShareTemplate = "ContactListShareTemplate.html";

        // Updating
        public const string ContactListUpdateSubject = "A gift list has been updated";
        public const string ContactListUpdateTemplate = "ContactListUpdateTemplate.html";
    }
}
