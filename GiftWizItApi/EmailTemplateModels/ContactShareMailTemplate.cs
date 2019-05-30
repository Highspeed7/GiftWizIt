using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.EmailTemplateModels
{
    public class ContactShareMailTemplate
    {
        public EmailAddress contactEmail { get; set; }
        public string fromUser { get; set; }
        public string giftListName { get; set; }
        public string giftListLink { get; set; }
        public string baseSiteLink { get; set; }
        public string giftListPassword { get; set; }
    }
}
