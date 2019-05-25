using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.EmailTemplateModels;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    public class Test
    {
        public string valueToSend { get; set; }
    }

    [Authorize]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IEmailService emailer;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ContactsController(IEmailService emailer, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.emailer = emailer;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IUnitOfWork UnitOfWork { get; }

        [Route("api/Contacts/Get")]
        [HttpGet]
        public async Task<IEnumerable<ContactUsersDTO>> GetAllUserContacts()
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var results = await unitOfWork.ContactUsers.GetAllUserContacts(userId);

            return mapper.Map<List<ContactUsersDTO>>(results);
        }

        [Route("api/Contacts/Add")]
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactDTO contact)
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            // Verify that the contact hasn't already been added
            var existingContact = await unitOfWork.Contacts.GetContactByEmail(contact.Email);

            if (existingContact != null)
            {
                // TODO: Utilize UI logic to prevent this from occurring to prevent more server calls.
                // Verify that the existing contact is not already a part of the users contacts
                var existingUserContact = await unitOfWork.ContactUsers.GetUserContactById(existingContact.ContactId, userId);
                if(existingUserContact != null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Contact already exists for user");
                }

                unitOfWork.ContactUsers.Add(contact, userId, mapper.Map<ContactDTO>(existingContact));
            }else
            {
                unitOfWork.ContactUsers.Add(contact, userId);
            }

            await unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK);
        }

        [Route("api/Contacts/Email")]
        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailAddress emailAddress)
        {
            var toAddresses = new List<EmailAddress>();
            toAddresses.Add(emailAddress);

            var fromAddresses = new List<EmailAddress>();
            fromAddresses.Add(new EmailAddress()
            {
                Name = "GiftWizIt",
                Address = "greetings@giftwizit.com"
            });

            var email = new EmailMessage()
            {
                ToAddresses = toAddresses,
                FromAddresses = fromAddresses,
                Content = "This is a test email",
                Subject = "Testing email"
            };

            await emailer.Send(email);

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}