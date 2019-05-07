using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public ItemsController(IWishListRepository repository, IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;    
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
            var wishLists = await _unitOfWork.WishLists.GetWishListsAsync(listName);
            
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
                insertedWishitem = _unitOfWork.WishItems.Add(userId, listName, item, existingWishList);
                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    try
                    {
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
    }
}