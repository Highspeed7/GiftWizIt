using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public GiftListsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
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
            
            foreach(GiftItemDTO item in giftItems)
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
                if(validItems.Count() == 0)
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
    }
}
