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
        private readonly IUserService userService;
        private readonly IGiftWizItWebSettings siteSettings;
        private ContactShareMailTemplate contactShareMailTemplate;

        public GiftListsController(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IHostingEnvironment env,
            IEmailService emailSender,
            IUserService userService,
            IGiftWizItWebSettings siteSettings)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.emailSender = emailSender;
            this.userService = userService;
            this.siteSettings = siteSettings;
        }

        [Route("api/GiftLists/")]
        [HttpPost]
        public async Task<ActionResult> CreateList(GiftListDto glist)
        {
            GiftLists insertedList = new GiftLists();
            // TODO: Check for valid user id's
            // TODO: Insure unique gift list names
            // TODO: If a previously deleted list name is the same as the one provided, re-enable it without items.
            var userId = await userService.GetUserIdAsync();

            // We want to set the userId incase this is a facebook login.
            glist.UserId = userId;

            // Check for user in database
            var user = await _unitOfWork.Users.GetUserByIdAsync(userId);

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
                _unitOfWork.Notifications.Add(new Notifications()
                {
                    UserId = userId,
                    Title = NotificationConstants.ListCreatedNotifTitle,
                    Message = $"Gift list {glist.Name} was created successfully!",
                    Type = NotificationConstants.Success,
                    CreatedOn = DateTime.Now
                });
                await _unitOfWork.CompleteAsync();
                return StatusCode((int)HttpStatusCode.OK, insertedList);
            }
            // TODO: Device custom status codes for different errors.
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [Route("api/GiftLists/")]
        [HttpGet]
        public async Task<IEnumerable<GiftLists>> GetUserGiftLists()
        {
            var userId = await userService.GetUserIdAsync();

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
            var userId = await userService.GetUserIdAsync();
            var result = await _unitOfWork.GiftItems.GetGiftListItems(gift_list_id, userId);

            return mapper.Map<List<QueryGiftItemDTO>>(result);
        }

        [Route("api/MoveGiftItem")]
        [HttpPost]
        public async Task<ActionResult> MoveItem(GiftItemMoveDTO[] giftItems)
        {
            var userId = await userService.GetUserIdAsync();

            // Validate the provided giftlist
            var giftLists = await _unitOfWork.GiftLists.GetUserLists(userId);

            foreach (GiftItemMoveDTO item in giftItems)
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
            var userId = await userService.GetUserIdAsync();

            // Get the user contacts for validation
            var contacts = await _unitOfWork.ContactUsers.GetAllUserContacts(userId);

            // Get the shared gift list
            // var giftList = await _unitOfWork.GiftLists.GetUserGiftListByIdAsync(userId, listShare.G_List_Id);

            // Queue for added lists
            Queue<SharedLists> addedLists = new Queue<SharedLists>();

            try
            {
                // For each contact in the provided contacts
                foreach (ContactDTO contact in listShare.Contacts)
                {
                    SharedLists sharedList = new SharedLists();
                    var dbContact = contacts.Where(c => c.ContactId == contact.ContactId).FirstOrDefault();

                    // Validate the provided contact is actually a contact of the user.
                    // If the contact is invalid
                    if (dbContact == null)
                    {
                        //Return Bad Request; Invalid Contact
                        return StatusCode((int)HttpStatusCode.BadRequest, "Invalid Contact");
                    }else
                    {
                        // Create a shared list object for insertion into the database
                        sharedList.GiftListId = listShare.G_List_Id;
                        sharedList.Password = listShare.Password;
                        sharedList.UserId = userId;
                        sharedList.ContactId = dbContact.Contact.ContactId;

                        // Queue the added lists to update their flags once email is sent.
                        var addedList = _unitOfWork.SharedLists.AddSharedList(sharedList);
                        addedLists.Enqueue(addedList);
                    }
                // END FOREACH
                }
                // COMPLETE UNIT OF WORK
                await _unitOfWork.CompleteAsync();

                // For each of the AddedLists
                foreach(SharedLists list in addedLists)
                {
                    // Instantiate email template and populate it's properties
                    contactShareMailTemplate = new ContactShareMailTemplate()
                    {
                        contactEmail = new EmailAddress()
                        {
                            Name = list.Contact.Name,
                            Address = list.Contact.Email
                        },
                        fromUser = User.Claims.First(e => e.Type == "name").Value,
                        giftListName = list.GiftList.Name,
                        giftListPassword = list.Password
                    };

                    if(env.IsDevelopment())
                    {
                        contactShareMailTemplate.giftListLink = $"{siteSettings.LocalBaseUrl}/shared-gift-list?gListId={list.GiftListId}&emailId={list.Contact.VerifyGuid}";
                        contactShareMailTemplate.baseSiteLink = $"{siteSettings.LocalBaseUrl}";
                    }
                    else
                    {
                        if(env.IsProduction() || env.IsStaging())
                        {
                            contactShareMailTemplate.giftListLink = $"{siteSettings.ProdBaseUrl}?giftId={list.GiftListId}";
                            contactShareMailTemplate.baseSiteLink = $"{siteSettings.ProdBaseUrl}";
                        }
                    }

                    // TODO: If the contact greet email has not been verified; don't send this one. Queue the greet email and this one.
                    try
                    {
                        // Send the email
                        await SendShareEmail(contactShareMailTemplate.contactEmail);
                        // Update the list's email sent flag to true
                        list.EmailSent = true;
                    }
                    catch (Exception ex)
                    {
                        // TODO: Implement logging and better error handling
                        // Return mixed success call 'One or more emails failed'
                        return StatusCode((int)HttpStatusCode.MultiStatus, "One or more emails failed");
                    }
                // END FOREACH
                }
                // COMPLETE UNIT OF WORK; UPDATING EMAIL FLAGS
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return StatusCode((int)HttpStatusCode.OK);
        }

        private string SetEmailTemplateParams(BodyBuilder builder)
        {
            /*
             {0}: Contact name
             {1}: From User
             {2}: Name of Gift List
             {3}: Link to Gift List
             {4}: Main Website Link
             {5}: GiftList Password
             */
            string messageBody = string.Format(builder.HtmlBody,
                contactShareMailTemplate.contactEmail.Name,
                contactShareMailTemplate.fromUser,
                contactShareMailTemplate.giftListName,
                contactShareMailTemplate.giftListLink,
                contactShareMailTemplate.baseSiteLink,
                contactShareMailTemplate.giftListPassword
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
