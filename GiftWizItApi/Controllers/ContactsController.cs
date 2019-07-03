using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Constants;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.EmailTemplateModels;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace GiftWizItApi.Controllers
{
    public class Test
    {
        public string valueToSend { get; set; }
    }

    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IEmailService emailer;
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment env;
        private ContactGreetMailTemplate contactMailTemplate;

        public ContactsController(
            IEmailService emailer,
            IUserService userService,
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IHostingEnvironment env)
        {
            this.emailer = emailer;
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
        }

        public IUnitOfWork UnitOfWork { get; }

        [Authorize]
        [Route("api/Contacts/Get")]
        [HttpGet]
        public async Task<IEnumerable<ContactUsersDTO>> GetAllUserContacts()
        {
            var userId = await userService.GetUserIdAsync();
            var results = await unitOfWork.ContactUsers.GetAllUserContacts(userId);

            return mapper.Map<List<ContactUsersDTO>>(results);
        }

        [Authorize]
        [Route("api/Contacts/Add")]
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactDTO contact)
        {
            var userId = await userService.GetUserIdAsync();

            ContactUsers insertedContact;

            // Check users table for the provided email
            var userContact = await unitOfWork.Users.GetUserByEmailAsync(contact.Email);

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

                insertedContact = unitOfWork.ContactUsers.Add(contact, userId, mapper.Map<ContactDTO>(existingContact));
            }else
            {
                insertedContact = unitOfWork.ContactUsers.Add(contact, userId);
            }

            if(userContact != null)
            {
                insertedContact.Contact.UserId = userContact.UserId;
            }

            try
            {
                // TODO: Move to function
                // Construct and send the email
                contactMailTemplate = new ContactGreetMailTemplate()
                {
                    contactEmail = new EmailAddress()
                    {
                        Address = contact.Email,
                        Name = contact.Name
                    }
                };
                await unitOfWork.CompleteAsync();

                contactMailTemplate.fromUser = User.Claims.First(e => e.Type == "name").Value;
                
                if(env.IsDevelopment())
                {
                    contactMailTemplate.getStartedUrl = $"{EmailTemplateConstants.ContactGetStartedDevUrl}?emailId={insertedContact.Contact.VerifyGuid}";
                }else
                {
                    if(env.IsProduction())
                    {
                        contactMailTemplate.getStartedUrl = $"{EmailTemplateConstants.ContactGetStartedProdUrl}?emailId={insertedContact.Contact.VerifyGuid}";
                    }
                }

                await SendGreetEmail();

                // Update the contact email column
                insertedContact.Contact.EmailSent = true;

                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implement better error handling
                // TODO: Logging here
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.MultiStatus, "Email failed with " + ex.Message);
            }

            return StatusCode((int)HttpStatusCode.OK);
        }

        [Route("api/Contacts/EmailVerify")]
        [HttpPost]
        public async Task<ContactDTO> VerifyEmail(string emailId)
        {
            Contacts contact = await unitOfWork.Contacts.GetContactByEmailGuid(emailId);
            if(contact != null)
            {
                contact.Verified = true;
                await unitOfWork.CompleteAsync();
            }
            return mapper.Map<ContactDTO>(contact);
        }

        private string SetEmailTemplateParams(BodyBuilder builder)
        {
            string messageBody = string.Format(builder.HtmlBody,
                contactMailTemplate.contactEmail.Name,
                contactMailTemplate.fromUser,
                contactMailTemplate.getStartedUrl
            );

            return messageBody;
        }

        private async Task SendGreetEmail()
        {
            var toAddresses = new List<EmailAddress>();
            toAddresses.Add(contactMailTemplate.contactEmail);

            var fromAddresses = new List<EmailAddress>
            {
                new EmailAddress()
                {
                    Name = EmailTemplateConstants.FromName,
                    Address = EmailTemplateConstants.FromAddress
                }
            };

            var email = new EmailMessage()
            {
                ToAddresses = toAddresses,
                FromAddresses = fromAddresses,
                Content = emailer.getContentBody<BodyBuilder>(EmailTemplateConstants.ContactGreetTemplate, SetEmailTemplateParams),
                Subject = EmailTemplateConstants.ContactGreetSubject
            };
            await emailer.Send(email);
        }
    }
}