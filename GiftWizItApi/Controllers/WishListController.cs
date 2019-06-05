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
    public class WishListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public WishListController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Route("api/WishList")]
        [HttpGet]
        public async Task<IEnumerable<WishListDto>> GetWishList()
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var result = await _unitOfWork.WishItems.GetWishItems(userId);

            return mapper.Map<List<WishListDto>>(result);
        }

        [Route("api/WishList/ItemDelete")]
        [HttpPost]
        public async Task<ActionResult> DeleteWishItem(ItemDTO[] items)
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var wishItems = await _unitOfWork.WishItems.GetWishItems(userId);

            foreach(ItemDTO item in items)
            {
                // Verify that the item is a part of the wishlist
                var validItem = wishItems.Where(wi => wi.Item_Id == item.Item_Id);

                if (validItem.Count() == 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, "Invalid Item Provided");
                }

                // Get WishItem Link
                WishItem wishItem = await _unitOfWork.WishItems.GetWishItemByItemId(item.Item_Id);

                wishItem.Deleted = true;
            }
            try
            {
                await _unitOfWork.CompleteAsync();
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
    }
}