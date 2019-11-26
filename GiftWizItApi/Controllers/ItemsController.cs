using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [Authorize]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public ItemsController(
            IWishListRepository repository,
            IUserService userService,
            IUnitOfWork unitOfWork, 
            IMapper mapper) {
            this.userService = userService;
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Route("api/Items")]
        [HttpPost]
        public async Task<ActionResult> CreateItem(ItemDTO item)
        {
            WishItem insertedWishitem = new WishItem();

            // Get the name of the user for the wish list
            var userId = await userService.GetUserIdAsync();
            var name = User.Claims.First(e => e.Type == "name").Value;
            var listName = $"{name}'s Wish List";

            // Filter the image url
            item.Image = FilterItemImageUrls(item.Image);

            // Check if wish list already exists
            WishLists existingWishList = await _unitOfWork.WishLists.GetWishListAsync(listName, userId);
            WishItem existingListItem = null;

            var partners = await _unitOfWork.Partners.GetPartnerAsync(item.Domain);
            var partner = partners.FirstOrDefault();
            if (partner == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Invalid Domain");
            }
            else
            {
                // TODO: Refactor towards cleaner architecture.
                Items newItem = new Items();

                // Map item dto to Items
                mapper.Map(item, newItem);

                bool isValidItemToAdd = false;

                // TODO: Refactor this.
                // Check for an existing item by domain unless it's null (null means it's a store item)
                if (item.Url != null)
                {
                    var wishItems = await validateProvidedItem(item.Url, userId);

                    if(wishItems.Count() == 0)
                    {
                        isValidItemToAdd = true;
                    }else
                    {
                        existingListItem = wishItems.Where(wi => wi.Deleted == true).FirstOrDefault();

                        if(existingListItem != null)
                        {
                            existingListItem.Deleted = false;
                            await _unitOfWork.CompleteAsync();
                            return StatusCode((int)HttpStatusCode.OK);
                        }
                    }
                }else
                {
                    isValidItemToAdd = true;
                    if(existingWishList != null)
                    {
                        existingListItem = existingWishList.WishItems.Where(wi => wi.Item.ProductId == item.ProductId).FirstOrDefault();
                        if (existingListItem != null)
                        {
                            if (existingListItem.Deleted == true)
                            {
                                existingListItem.Deleted = false;
                                await _unitOfWork.CompleteAsync();
                                return StatusCode((int)HttpStatusCode.OK);
                            }
                            else
                            {
                                isValidItemToAdd = false;
                            }
                        }
                    }
                }

                if (!isValidItemToAdd)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "The added item already exists for the user");
                }


                insertedWishitem = _unitOfWork.WishItems.Add(userId, listName, newItem, existingWishList);

                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    try
                    {
                        // Check to make sure the affiliate link isn't already in the items list.
                        // Now update the link-items-partners table
                        // Append the affiliate tag to the end of the url
                        var strLen = item.Url.Count();

                        _unitOfWork.LnksItmsPtns.Add(item.Url, insertedWishitem.Item.Item_Id, partner.PartnerId);
                        await _unitOfWork.CompleteAsync();
                    }
                    catch (Exception e)
                    {
                        // TODO: Remove previously inserted wishItem
                    }
                }
            }
            return StatusCode((int)HttpStatusCode.OK);
        }

        [Route("api/MoveItems")]
        [HttpPost]
        public async Task<ActionResult> ItemToGiftList(GiftItemMoveDTO[] items)
        {
            // Validate user is allowed to add item to giftlist and that Gift List is valid
            var userId = await userService.GetUserIdAsync();
           
            // Get user lists
            IEnumerable<GiftLists> giftLists = await _unitOfWork.GiftLists.GetUserLists(userId);
            IEnumerable<WishListRaw> wish_items = await _unitOfWork.WishItems.GetWishItems(userId);
            
            foreach (var item in items)
            {
                // TODO: Validate gift lists later
                // Validate Gift List
                // var validGiftList = giftLists.Where(gl => gl.Id == item.G_List_Id);
                // Validate Item
                var validItems = wish_items.Where(wi => wi.Item_Id == item.Item_Id);

                // Get WishItem Link
                WishItem wishItem = await _unitOfWork.WishItems.GetWishItemByItemId(item.Item_Id);
                // TODO: Add this back to the if statement, below, later : validGiftList.Count() == 0 || 
                if (validItems.Count() == 0)
                {
                    // TODO: Implement logging
                    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid Gift List or Items");
                }else
                {
                    GiftItem newGiftItem = new GiftItem
                    {
                        GListId = item.G_List_Id,
                        Item_Id = item.Item_Id
                    };

                    _unitOfWork.GiftItems.Add(newGiftItem);
                    // Update wishItem to deleted state
                    wishItem.Deleted = true;
                }
            }
            // Add item to giftlist.
            var giftItemResults = await _unitOfWork.CompleteAsync();
            
            if(giftItemResults > 0 )
            {
                // Delete from wish item list.

            }
            return StatusCode((int)HttpStatusCode.OK);
        }

        private async Task<IEnumerable<WishItem>> validateProvidedItem(string itemUrl, string userId)
        {
            IEnumerable<WishItem> result = await _unitOfWork.WishItems.GetWishItemByUrl(itemUrl, userId);

            var items = result.Where(r => r.Item.LinkItemPartners.Where(lip => lip.AffliateLink == itemUrl).Count() > 0);

            return items;
        }

        private string FilterItemImageUrls(string imageUrl)
        {
            string[] disallowedValues = {
                "FMwebp_"
            };

            foreach(string value in disallowedValues)
            {
                imageUrl = imageUrl.Replace(value, "");
            }

            return imageUrl;
        }
    }
}