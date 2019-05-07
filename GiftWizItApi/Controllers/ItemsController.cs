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
        public async Task<ActionResult> CreateItem(Items item)
        {
            // Get the name of the user
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var name = User.Claims.First(e => e.Type == "name").Value;
            var listName = $"{name}'s Wish List";

            _unitOfWork.WishLists.Add(userId, listName, item);

            await _unitOfWork.CompleteAsync();
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}