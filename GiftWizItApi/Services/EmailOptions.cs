using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Service
{
    public class EmailOptions
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string EmailTitle { get; set; }
        public string ContactGreetTemplateId { get; set; }
    }
}
