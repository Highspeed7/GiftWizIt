﻿using AutoMapper;
using GiftWizItApi.Constants;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Controllers.dtos.notifications;
using GiftWizItApi.EmailTemplateModels;
using GiftWizItApi.Extensions;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using GiftWizItApi.SignalR.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
    [ApiController]
    public class GiftListsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment env;
        private readonly IEmailService emailSender;
        private readonly IUserService userService;
        private readonly IGiftWizItWebSettings siteSettings;
        private readonly IHubContext<MainHub> _hubContext;
        private ContactShareMailTemplate contactShareMailTemplate;

        public GiftListsController(
            IHubContext<MainHub> hubContext,
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
            this._hubContext = hubContext;
            this.emailSender = emailSender;
            this.userService = userService;
            this.siteSettings = siteSettings;
        }

        [Authorize]
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

                var notification = new ListCreatedNotificationDTO()
                {
                    NotificationTitle = NotificationConstants.ListCreatedNotifTitle
                };

                await _hubContext.Clients.Group(userId).SendAsync("Notification", notification);

                return StatusCode((int)HttpStatusCode.OK, insertedList);
            }
            // TODO: Device custom status codes for different errors.
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [Authorize]
        [Route("api/GiftLists/")]
        [HttpGet]
        public async Task<IEnumerable<GiftLists>> GetUserGiftLists()
        {
            // TODO: Move toward clean architecture
            var userId = await userService.GetUserIdAsync();

            var giftLists = await _unitOfWork.GiftLists.GetUserLists(userId);
            
            return giftLists;
        }

        [Authorize]
        [Route("api/GiftLists/")]
        [HttpDelete]
        public async Task<ActionResult> DeleteList(GiftListDto glist)
        {
            // TODO: Delete items associated with the list.

            await _unitOfWork.GiftLists.DeleteGiftList(glist.Id);
            var result = await _unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Authorize]
        [Route("api/GiftListItems/")]
        [HttpGet]
        public async Task<IEnumerable<QueryGiftItemDTO>> GetGiftListItems(int gift_list_id)
        {
            var userId = await userService.GetUserIdAsync();
            var result = await _unitOfWork.GiftItems.GetGiftListItems(gift_list_id, true, userId);

            return mapper.Map<List<QueryGiftItemDTO>>(result);
        }

        [Authorize]
        [Route("api/GiftListItems")]
        [HttpPost]
        public async Task<ActionResult> DeleteListItem(QueryGiftItemDTO[] giftItems)
        {
            var userId = await userService.GetUserIdAsync();
            // Verify that the user had access to the provided gift list id.
            // Since items can only be deleted from one list at a time, we'll take the first item's list id.
            var giftList = await _unitOfWork.GiftLists.GetUserGiftListByIdAsync(userId, giftItems[0].Gift_List_Id);
            if (giftList == null)
            {
                // TODO: Eliminate magic strings.
                return StatusCode((int)HttpStatusCode.BadRequest, "Gift list not found for this user.");
            } else
            {
                // Verify that the item is a part of the gift list
                foreach (QueryGiftItemDTO item in giftItems)
                {
                    var dbGiftItem = await _unitOfWork.GiftItems.GetGiftItemByIdAsync(item.Item_Id);

                    if (dbGiftItem == null)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, "The provided item does not exist");
                    } else
                    {
                        // We check to make sure the provided item id is a part of the provided gift list.
                        if (dbGiftItem.GListId != item.Gift_List_Id)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, "The item is either not a part of the provided list or it has already been deleted.");
                        }
                        // Soft delete the item.
                        dbGiftItem.Deleted = true;
                    }
                }
                var result = await _unitOfWork.CompleteAsync();
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            // Set the deleted flag to true
        }

        //[Route("api/GiftListItems/Purchase")]
        //[HttpGet]
        //public async Task<ActionResult> SetItemPurchaseStatus(string item_id)
        //{
        //    return StatusCode((int)HttpStatusCode.OK);
        //}

        [Authorize]
        [Route("api/MoveGiftItem")]
        [HttpPost]
        public async Task<ActionResult> MoveItem(GiftItemMoveDTO[] giftItems)
        {
            var userId = await userService.GetUserIdAsync();

            // Validate the provided giftlist
            var giftLists = await _unitOfWork.GiftLists.GetUserLists(userId);

            foreach (GiftItemMoveDTO item in giftItems)
            {
                // TODO: Add the below back later removing for now to allow free movement between shared lists.
                // Validate the gift list destination is valid
                //var validDestGiftList = giftLists.FirstOrDefault(gl => gl.Id == item.To_Glist_Id);

                //if (validDestGiftList == null)
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid destination giftlist provided");
                //}
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

        [Authorize]
        [Route("api/ShareGiftList")]
        [HttpPost]
        public async Task<ActionResult> ShareGiftList(GListShareDTO listShare)
        {
            // Get the userid
            var userId = await userService.GetUserIdAsync();
            var userName = User.Claims.First(e => e.Type == "name").Value;
            var userEmail = User.Claims.First(e => e.Type == "emails").Value;

            // Get the user contacts for validation
            var contacts = await _unitOfWork.ContactUsers.GetAllUserContacts(userId);

            // Get the shared gift list
            var giftList = await _unitOfWork.GiftLists.GetUserGiftListByIdAsync(userId, listShare.G_List_Id);

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
                    } else
                    {
                        // Create a shared list object for insertion into the database
                        sharedList.GiftListId = listShare.G_List_Id;
                        //sharedList.Password = (listShare.Password == null) ? "" : listShare.Password;
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
                foreach (SharedLists list in addedLists)
                {
                    // Instantiate email template and populate it's properties
                    contactShareMailTemplate = new ContactShareMailTemplate()
                    {
                        contactEmail = new EmailAddress()
                        {
                            Name = list.Contact.Name,
                            Address = list.Contact.Email
                        },
                        fromUser = userName,
                        giftListName = list.GiftList.Name,
                        giftListPassword = giftList.Password
                    };

                    if (env.IsDevelopment())
                    {
                        contactShareMailTemplate.giftListLink = $"{siteSettings.LocalBaseUrl}/shared-gift-list?gListId={list.GiftListId}&emailId={list.Contact.VerifyGuid}";
                        contactShareMailTemplate.baseSiteLink = $"{siteSettings.LocalBaseUrl}";
                    }
                    else
                    {
                        if (env.IsProduction() || env.IsStaging())
                        {
                            contactShareMailTemplate.giftListLink = $"{siteSettings.ProdBaseUrl}/shared-gift-list?gListId={list.GiftListId}&emailId={list.Contact.VerifyGuid}";
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

                        // If the Contact is a user on this system
                        if (list.Contact.UserId != null)
                        {
                            _unitOfWork.Notifications.Add(new Notifications()
                            {
                                UserId = list.Contact.UserId,
                                Title = NotificationConstants.ListShareSuccessNotifTitle,
                                Message = $"The gift list '{list.GiftList.Name}' has been shared with you. " +
                                $"{(list.GiftList.IsPublic ? "There is no password assigned to this list." : $"It's password is: {list.GiftList.Password}.")}{Environment.NewLine}" +
                                $"To view the contents of this list, navigate to your 'Other lists' page.",
                                Type = NotificationConstants.InfoType,
                                CreatedOn = DateTime.Now
                            });

                            //List<string> currentUserConnectionIds = new List<string>();

                            //NotificationsHub.ConnectedUsers.TryGetValue(userEmail, out currentUserConnectionIds);

                            //foreach(string id in currentUserConnectionIds)
                            //{
                            //    await _hubContext.Groups.AddToGroupAsync(id, list.Contact.UserId);
                            //}

                            var notification = new SharedListNotificationDTO()
                            {
                                NotificationTitle = NotificationConstants.ListShareSuccessNotifTitle
                            };

                            await _hubContext.Clients.Group(list.Contact.UserId).SendAsync("Notification", notification);
                        }

                    }
                    catch (Exception ex)
                    {
                        // TODO: Implement logging and better error handling
                        _unitOfWork.Notifications.Add(new Notifications()
                        {
                            UserId = userId,
                            Title = NotificationConstants.ListShareFailedNotifTitle,
                            Message = $"One or more of your contacts did not receive the gift list '{list.GiftList.Name}'" +
                            $"{Environment.NewLine} return to share the list; adding whichever contact you intended to recieve it." +
                            $"{Environment.NewLine} you may also want to verify their email before doing so.",
                            Type = NotificationConstants.WarningType,
                            CreatedOn = DateTime.Now
                        });

                        await _hubContext.Clients.Group(userId).SendAsync("Notification", NotificationConstants.ListShareFailedNotifTitle);

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

        [Route("api/SearchPublicLists")]
        [HttpPost]
        public async Task<ActionResult> GetSearchedLists(SearchTermDTO search)
        {
            string userId = null;
            string term = search.SearchTerm;
            Users user;

            if (term == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No Search term provided");
            }

            if (search.Pager == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No paging data provided");
            }

            if (search.UserEmail != null)
            {
                user = await this._unitOfWork.Users.GetUserByEmailAsync(search.UserEmail);
                userId = user.UserId;
            }

            var mappedPager = mapper.Map<Page>(search.Pager);

            var result = await this._unitOfWork.GiftLists.GetPublicGiftListsBySearch(term, mappedPager, userId);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        // TODO: Somewhat of a security issue, unimportant though as it's practically useless to exploit.
        [Route("api/SearchPrivateListItems")]
        [HttpGet]
        public async Task<ActionResult> GetSearchedPrivateListItems(int giftListId)
        {
            var result = await _unitOfWork.GiftItems.GetPrivateGiftListItems(giftListId);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Route("api/SearchPublicListItems")]
        [HttpGet]
        public async Task<ActionResult> GetSearchedPublicListItems(int giftListId)
        {
            var result = await _unitOfWork.GiftItems.GetGiftListItems(giftListId, true);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Route("api/SearchPrivateLists")]
        [HttpPost]
        public async Task<ActionResult> GetSearchedPrivateLists(SearchTermDTO search)
        {
            string userId = null;
            string term = search.SearchTerm;
            string password = search.Password;

            Users user;

            if(password == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No password provided for private search");
            }

            if(term == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No Search term provided");
            }
            
            if(search.UserEmail == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No email provided");
            }

            user = await this._unitOfWork.Users.GetUserByEmailAsync(search.UserEmail);

            if(user == null)
            {
                return StatusCode((int)HttpStatusCode.OK);
            }

            userId = user.UserId;

            var mappedPager = mapper.Map<Page>(search.Pager);

            var result = await this._unitOfWork.GiftLists.GetPrivateGiftListsBySearch(term, mappedPager, search.Password, userId);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Authorize]
        [Route("api/GiftLists/Update")]
        [HttpPost]
        public async Task<ActionResult> UpdateGiftList(EditContactDTO giftList) {
            var userId = await userService.GetUserIdAsync();
            var dbGiftList = await _unitOfWork.GiftLists.GetUserGiftListByIdAsync(userId, giftList.GiftListId);
            var dbSharedList = await _unitOfWork.SharedLists.GetUserSharedListCollection(userId, giftList.GiftListId);

            dbGiftList.Name = giftList.NewName;
            dbGiftList.Password = (giftList.NewPass == null) ? "" : giftList.NewPass;
            dbGiftList.IsPublic = giftList.IsPublic;
            dbGiftList.RestrictChat = giftList.RestrictChat;
            dbGiftList.AllowItemAdds = giftList.AllowAdds;

            foreach (SharedLists list in dbSharedList)
            {
                // Update contacts with new password.
                // We'll use the same share mail template, because they're similar.
                contactShareMailTemplate = new ContactShareMailTemplate()
                {
                    contactEmail = new EmailAddress()
                    {
                        Name = list.Contact.Name,
                        Address = list.Contact.Email
                    },
                    fromUser = User.Claims.First(e => e.Type == "name").Value,
                    giftListName = giftList.NewName,
                    giftListPassword = (giftList.NewPass == null) ? "No password has been set for this list." : giftList.NewPass
                };

                if (env.IsDevelopment())
                {
                    contactShareMailTemplate.giftListLink = $"{siteSettings.LocalBaseUrl}/shared-gift-list?gListId={list.GiftListId}&emailId={list.Contact.VerifyGuid}";
                    contactShareMailTemplate.baseSiteLink = $"{siteSettings.LocalBaseUrl}";
                }
                else
                {
                    if (env.IsProduction() || env.IsStaging())
                    {
                        contactShareMailTemplate.giftListLink = $"{siteSettings.ProdBaseUrl}?giftId={list.GiftListId}";
                        contactShareMailTemplate.baseSiteLink = $"{siteSettings.ProdBaseUrl}";
                    }
                }
            }

            var saveResults = await _unitOfWork.CompleteAsync();

            if (saveResults == 0)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Update failed");
            } else
            {
                foreach (SharedLists list in dbSharedList)
                {
                    try
                    {
                        await SendContactUpdateEmail(new EmailAddress()
                        {
                            Name = list.Contact.Name,
                            Address = list.Contact.Email
                        });
                    } catch (Exception e)
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, $"Sending email failed with: {e.Message}");
                    }

                }
                await _hubContext.Clients.User(userId).SendAsync("Notification", $"Testing notifications");
                return StatusCode((int)HttpStatusCode.OK, dbGiftList);
            }
        }

        [Authorize]
        [Route("api/GiftLists/Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteGiftLists(GiftListDto[] giftLists)
        {
            var userId = await userService.GetUserIdAsync();

            var dbGiftLists = await _unitOfWork.GiftLists.GetUserLists(userId);
            
            foreach(GiftListDto list in giftLists)
            {
                var validList = dbGiftLists.Where(gl => gl.Id == list.Id).FirstOrDefault();
                if(validList == null)
                {
                    await _hubContext.Clients.Group(userId).SendAsync("Notification", "Could not delete lists, an invalid gift list id was provided");
                    _unitOfWork.Notifications.Add(new Notifications()
                    {
                        UserId = userId,
                        Title = "Deletion of Gift List(s) failed",
                        Message = "An Invalid gift list id was provided",
                        Type = NotificationConstants.WarningType,
                        CreatedOn = DateTime.Now
                    });

                    await _unitOfWork.CompleteAsync();
                    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid gift list id provided");
                }

                // Get the items and delete those as well
                var listItems = await _unitOfWork.GiftItems.GetRawGiftListItems(validList.Id);

                foreach(GiftItem item in listItems)
                {
                    item.Deleted = true;
                }

                validList.Deleted = true;
            }
            var result = await _unitOfWork.CompleteAsync();
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        private string SetShareEmailTemplateParams(BodyBuilder builder)
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

        private async Task SendContactUpdateEmail(EmailAddress toAddresses)
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
                Content = emailSender.getContentBody<BodyBuilder>(EmailTemplateConstants.ContactListUpdateTemplate, SetShareEmailTemplateParams),
                Subject = EmailTemplateConstants.ContactListUpdateSubject
            };

            email.ToAddresses.Add(toAddresses);

            await emailSender.Send(email);
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
                Content = emailSender.getContentBody<BodyBuilder>(EmailTemplateConstants.ContactListShareTemplate, SetShareEmailTemplateParams),
                Subject = EmailTemplateConstants.ContactListShareSubject
            };

            email.ToAddresses.Add(toAddresses);

            await emailSender.Send(email);
        }
    }
}
