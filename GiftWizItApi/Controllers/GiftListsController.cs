using AutoMapper;
using GiftWizItApi.Constants;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.EmailTemplateModels;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers
{
    [Authorize]
    [ApiController]
    public class GiftListsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment env;
        private readonly IEmailService emailSender;
        private ContactShareMailTemplate contactShareMailTemplate;

        public GiftListsController(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IHostingEnvironment env,
            IEmailService emailSender)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.emailSender = emailSender;
        }

        [Route("api/GiftLists/")]
        [HttpPost]
        public async Task<ActionResult> CreateList(GiftListDto glist)
        {
            GiftLists insertedList = new GiftLists();
            // TODO: Check for valid user id's
            // TODO: Insure unique gift list names
            // TODO: If a previously deleted list name is the same as the one provided, re-enable it without items.

            // Check for user in database
            var user = await _unitOfWork.Users.GetUserByIdAsync(glist.UserId);

            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            else
            {
                insertedList = _unitOfWork.GiftLists.Add(glist);
            }

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
            {
                return StatusCode((int)HttpStatusCode.OK, insertedList);
            }
            // TODO: Device custom status codes for different errors.
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [Route("api/GiftLists/")]
        [HttpGet]
        public async Task<IEnumerable<GiftLists>> GetUserGiftLists()
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            return await _unitOfWork.GiftLists.GetUserLists(userId);
        }

        [Route("api/GiftLists/")]
        [HttpDelete]
        public async Task<ActionResult> DeleteList(GiftListDto glist)
        {
            // TODO: Delete items associated with the list.

            await _unitOfWork.GiftLists.DeleteGiftList(glist.Id);
            var result = await _unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Route("api/GiftListItems/")]
        [HttpGet]
        public async Task<IEnumerable<QueryGiftItemDTO>> GetGiftListItems(int gift_list_id)
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var result = await _unitOfWork.GiftItems.GetGiftListItems(gift_list_id, userId);

            return mapper.Map<List<QueryGiftItemDTO>>(result);
        }

        [Route("api/MoveGiftItem")]
        [HttpPost]
        public async Task<ActionResult> MoveItem(GiftItemDTO[] giftItems)
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            // Validate the provided giftlist
            var giftLists = await _unitOfWork.GiftLists.GetUserLists(userId);

            foreach (GiftItemDTO item in giftItems)
            {
                // Validate the gift list destination is valid
                var validDestGiftList = giftLists.FirstOrDefault(gl => gl.Id == item.To_Glist_Id);

                if (validDestGiftList == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid destination giftlist provided");
                }
                // Validate the item
                var itemsToCheck = giftLists.SelectMany(gl => gl.GiftItems);
                var validItems = itemsToCheck.Where(i => i.Item_Id == item.Item_Id);
                if (validItems.Count() == 0)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Item does not exist in this context.");
                }

                // Should never return null due to the above validation.
                var dbGiftItem = await _unitOfWork.GiftItems.GetGiftItemByIdAsync(item.Item_Id);

                _unitOfWork.GiftItems.Remove(item.G_List_Id, item.Item_Id);

                var mappedItem = mapper.Map<GiftItem>(item);

                _unitOfWork.GiftItems.Add(mappedItem);
            }

            var result = await _unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Route("api/ShareGiftList")]
        [HttpPost]
        public async Task<ActionResult> ShareGiftList(GListShareDTO listShare)
        {
            // Get the userid
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            var contacts = await _unitOfWork.ContactUsers.GetAllUserContacts(userId);

            List<EmailAddress> emailsToSend = new List<EmailAddress>();

            Queue<SharedLists> addedLists = new Queue<SharedLists>();

            foreach (ContactDTO contact in listShare.Contacts)
            {
                SharedLists sharedList = new SharedLists();
                var dbContact = contacts.Where(c => c.ContactId == contact.ContactId).FirstOrDefault();

                // Validate the provided contact is actually a contact of the user.
                if (dbContact == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid Contact");
                }
                else
                {
                    sharedList.GiftListId = listShare.G_List_Id;
                    sharedList.Password = listShare.Password;
                    sharedList.UserId = userId;
                    sharedList.ContactId = dbContact.Contact.ContactId;
                }
                // Queue the added lists to update their flags once email is sent.
                var addedList = _unitOfWork.SharedLists.AddSharedList(sharedList);
                addedLists.Enqueue(addedList);

                // Add contact's email to the email list.
                emailsToSend.Add(new EmailAddress()
                {
                    Address = dbContact.Contact.Email,
                    Name = dbContact.Contact.Name
                });
            }
            try
            {
                await _unitOfWork.CompleteAsync();

                foreach(var recpnt in emailsToSend)
                {
                    // TODO: Move to it's own function.
                    // Set the template values for this contact
                    contactShareMailTemplate = new ContactShareMailTemplate()
                    {
                        contactEmail = new EmailAddress()
                        {
                            Name = recpnt.Name,
                            Address = recpnt.Address
                        }
                    };
                    contactShareMailTemplate.fromUser = User.Claims.First(e => e.Type == "name").Value;

                    /* TODO: If the contact greet email has not been verified; don't send this one. 
                    * Queue the greet email and this one. */
                    await SendShareEmail(recpnt);
                }

                // Now that emails are sent
                // Set the emailSent flags of each shared list to true
                foreach(SharedLists list in addedLists)
                {
                    list.EmailSent = true;
                }
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                // TODO: Implement better error handling
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return StatusCode((int)HttpStatusCode.OK);
        }

        private string SetEmailTemplateParams(BodyBuilder builder)
        {
            string messageBody = string.Format(builder.HtmlBody,
                contactShareMailTemplate.contactEmail.Name,
                contactShareMailTemplate.fromUser
            );

            return messageBody;
        }

        private async Task SendShareEmail(EmailAddress toAddresses)
        {
            var fromAddress = new List<EmailAddress>();
            fromAddress.Add(new EmailAddress()
            {
                Name = EmailTemplateConstants.FromName,
                Address = EmailTemplateConstants.FromAddress
            });

            var email = new EmailMessage()
            {
                FromAddresses = fromAddress,
                Content = emailSender.getContentBody<BodyBuilder>(EmailTemplateConstants.ContactListShareTemplate, SetEmailTemplateParams),
                Subject = EmailTemplateConstants.ContactListShareSubject
            };

            email.ToAddresses.Add(toAddresses);

            await emailSender.Send(email);
        }

        
    }
}
