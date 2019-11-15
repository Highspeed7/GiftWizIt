using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemClaimsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;

        public ItemClaimsController(IUnitOfWork unitOfWork, IUserService userService)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
        }

        [Authorize]
        [Route("ClaimItem")]
        [HttpPost]
        public async Task<ActionResult> ClaimListItem(int item_id, int list_id)
        {
            var userId = await userService.GetUserIdAsync();

            var itemClaim = await unitOfWork.ItemClaims.GetItemClaim(item_id, list_id);

            if(itemClaim != null)
            {
                itemClaim.Closed = false;
            }else
            {
                unitOfWork.ItemClaims.Add(new ItemClaims()
                {
                    GiftListId = list_id,
                    ItemId = item_id,
                    UserId = userId
                });
            }

            var result = await unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Authorize]
        [Route("UnclaimItem")]
        [HttpPost]
        public async Task<ActionResult> UnclaimListItem(int item_id, int list_id)
        {
            var itemClaim = await unitOfWork.ItemClaims.GetItemClaim(item_id, list_id);

            if(itemClaim == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "No claim for the given item");
            }

            itemClaim.Closed = true;

            var result = await unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }
    }
}