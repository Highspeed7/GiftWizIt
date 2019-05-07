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

        public GiftListsController(IGiftListRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("api/GiftLists/")]
        [HttpPost]
        public async Task<ActionResult> CreateList(GiftListDto glist)
        {
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
                _unitOfWork.GiftLists.Add(glist);
            }

            var result = await _unitOfWork.CompleteAsync();
            if (result > 0)
            {
                return StatusCode((int)HttpStatusCode.OK, result);
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
    }
}
