using GiftWizItApi.Interfaces;
using GiftWizItApi.SignalR.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers
{
    [Authorize]
    [ApiController]
    public class ItemChatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IHubContext<MainHub> _hubContext;

        public ItemChatController(
            IUnitOfWork unitOfWork,
            IUserService userService,
            IHubContext<MainHub> hubContext
        )
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _hubContext = hubContext;
        }

        //[Route("api/ItemChatChannel)")]
        //[HttpPost]
        //public async Task<ActionResult> ConnectToListChatChannel(string connectionId, int giftListId, string password)
        //{
        //    string userId = await _userService.GetUserIdAsync();

        //    // Get the lists that have been shared with this user.
        //        // If the specified giftlist id is not a part of the retrieved lists
        //            // Return an unauthorized response.
        //        // Else 
        //            // Get the specified gift list
        //            // Connect the user to the channel of the gift list specified
        //    //try
        //    //{
        //    //    await _hubContext.Groups.AddToGroupAsync(connectionId, sharedListId.ToString());
        //    //    return StatusCode((int)HttpStatusCode.OK, "Connected to List Chat Channel");
        //    //}
        //}
    }
}
