using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Constants;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Controllers.dtos.notifications;
using GiftWizItApi.EmailTemplateModels;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using GiftWizItApi.SignalR.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<MainHub> hubContext;
        private ContactGreetMailTemplate contactMailTemplate;

        public ContactsController(
            IEmailService emailer,
            IUserService userService,
            IUnitOfWork unitOfWork,
            IHubContext<MainHub> hubContext,
            IMapper mapper,
            IHostingEnvironment env)
        {
            this.hubContext = hubContext;
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
        [Route("api/Contacts/Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteUserContact(ContactDTO[] contacts)
        {
            var userId = await userService.GetUserIdAsync();
            var dbContacts = await unitOfWork.ContactUsers.GetAllUserContacts(userId);

            foreach (ContactUsers dbContact in dbContacts)
            {
                foreach (ContactDTO contact in contacts)
                {
                    if (contact.ContactId != dbContact.ContactId)
                    {
                        continue;
                    }else
                    {
                        dbContact.Deleted = true;
                    }
                }
            }

            unitOfWork.Notifications.Add(new Notifications()
            {
                UserId = userId,
                Title = NotificationConstants.ContactDeleteSuccessNotifTitle,
                Message = $"You successfully deleted {contacts.Count()} contact(s).",
                Type = NotificationConstants.InfoType,
                CreatedOn = DateTime.Now
            });

            var notification = new ContactDeletedNotificationDTO()
            {
                NotificationTitle = NotificationConstants.ContactDeleteSuccessNotifTitle
            };

            await hubContext.Clients.Group(userId).SendAsync("Notification", notification);

            var result = await unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Authorize]
        [Route("api/Contacts/Add")]
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactDTO contact)
        {
            var userId = await userService.GetUserIdAsync();
            var userName = User.Claims.First(e => e.Type == "name").Value;

            // Make sure the email is all lowercase.
            contact.Email = contact.Email.ToLower();

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
                    if(existingUserContact.Deleted == true)
                    {
                        try
                        {
                            existingUserContact.Deleted = false;

                            var notification = new ContactAddedNotificationDTO()
                            {
                                NotificationTitle = NotificationConstants.ContactAddedNotifTitle
                            };

                            await unitOfWork.CompleteAsync();

                            unitOfWork.Notifications.Add(new Notifications()
                            {
                                UserId = userId,
                                Title = NotificationConstants.ContactAddSuccessNotifTitle,
                                Message = $"You successfully Added {contact.Name} as a contact.",
                                Type = NotificationConstants.InfoType,
                                CreatedOn = DateTime.Now
                            });

                            await unitOfWork.CompleteAsync();

                            await hubContext.Clients.Group(userId).SendAsync("Notification", notification);

                            return StatusCode((int)HttpStatusCode.OK);
                        }catch(Exception e)
                        {
                            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                        }
                    }
                }

                ContactDTO contactToAdd = mapper.Map<ContactDTO>(existingContact);

                contactToAdd.Alias = contact.Name;

                insertedContact = unitOfWork.ContactUsers.Add(contact, userId, contactToAdd);
            }else
            {
                insertedContact = unitOfWork.ContactUsers.Add(contact, userId);
            }

            if(userContact != null)
            {
                insertedContact.Contact.UserId = userContact.UserId;

                // Notify the user via notifications
                unitOfWork.Notifications.Add(new Notifications()
                {
                    UserId = userContact.UserId,
                    Title = NotificationConstants.ContactAddedNotifTitle,
                    Message = $"{userName} has added you as a contact",
                    Type = NotificationConstants.InfoType,
                    CreatedOn = DateTime.Now
                });

                var notification = new ContactAddedNotificationDTO()
                {
                    NotificationTitle = NotificationConstants.ContactAddedNotifTitle
                };

                await hubContext.Clients.Group(userId).SendAsync("Notification", notification);
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

                contactMailTemplate.fromUser = userName;
                
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