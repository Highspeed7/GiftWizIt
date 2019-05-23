using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GiftWizItApi.EmailTemplateModels;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    public class Test
    {
        public string valueToSend { get; set; }
    }

    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactEmailer emailer;

        public ContactsController(IContactEmailer emailer)
        {
            this.emailer = emailer;
        }

        [Route("api/Contacts/Email")]
        [HttpPost]
        public async Task<IActionResult> SendEmail(Test valueToSend)
        {
            var to = "brwest@enlistedinnovations.com";

            ContactMailTemplate templateData = new ContactMailTemplate()
            {
                contactName = "Gretchen",
                from = "Brian West",
                getStartedUrl = "https://www.giftwizit.com"
            };

            await emailer.SendEmailTransactionalAsync(to, templateData);
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}