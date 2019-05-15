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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [Authorize]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public ItemsController(IWishListRepository repository, IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Route("api/Items")]
        [HttpPost]
        public async Task<ActionResult> CreateItem(ItemDTO item)
        {
            WishItem insertedWishitem = new WishItem();
            WishLists existingWishList = null;

            // Get the name of the user for the wish list
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var name = User.Claims.First(e => e.Type == "name").Value;
            var listName = $"{name}'s Wish List";
            // Check if wish list already exists
            var wishLists = await _unitOfWork.WishLists.GetWishListsAsync(listName, userId);
            
            // If Wish list exists
            if(wishLists.Count() > 0)
            {
                // Set the wishlist to send to WishListItems repository.
                existingWishList = wishLists.First();
            }

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
                // Convert to Items Object
                // TODO: Check to make sure item doesn't already exist in item's table.
                insertedWishitem = _unitOfWork.WishItems.Add(userId, listName, newItem, existingWishList);
                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    try
                    {
                        // Check to make sure the affiliate link isn't already in the items list.
                        // Now update the link-items-partners table
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
        public async Task<ActionResult> ItemToGiftList(GiftItemDTO[] items)
        {
            // Validate user is allowed to add item to giftlist and that Gift List is valid
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
           
            // Get user lists
            IEnumerable<GiftLists> giftLists = await _unitOfWork.GiftLists.GetUserLists(userId);
            IEnumerable<WishListRaw> wish_items = await _unitOfWork.WishItems.GetWishItem(userId);
            
            foreach (var item in items)
            {
                // Validate Gift List
                var validGiftList = giftLists.Where(gl => gl.Id == item.G_List_Id);
                // Validate Item
                var validItems = wish_items.Where(wi => wi.Item_Id == item.Item_Id);

                // Get WishItem Link
                WishItem wishItem = await _unitOfWork.WishItems.GetWishItemByItemId(item.Item_Id);
                
                if(validGiftList.Count() < items.Count() || validItems.Count() < items.Count())
                {
                    // TODO: Implement logging
                    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid Gift Lists or Items");
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
                    //_unitOfWork.WishItems.Update(wishItem);
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
    }
}