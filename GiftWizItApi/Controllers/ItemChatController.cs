using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using GiftWizItApi.SignalR.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers
{
    [ApiController]
    public class ItemChatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> _hubContext;

        public ItemChatController(
            IUnitOfWork unitOfWork,
            IUserService userService,
            IHubContext<ChatHub> hubContext
        )
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _hubContext = hubContext;
        }

        [Authorize]
        [Route("api/ListChatChannel")]
        [HttpPost]
        public async Task<ActionResult> ConnectToListChatChannel(ItemChatConnectDTO connectionData)
        {
            string userId = await _userService.GetUserIdAsync();

            // Get the lists that have been shared with this user.
            var usersSharedLists = await _unitOfWork.SharedLists.GetAllUserSharedLists(userId);

            // Get the specified gift list
            var list = usersSharedLists.Where(l => l.GiftListId == connectionData.GiftListId);

            // If the specified giftlist id is not a part of the retrieved lists
            if(list == null)
            {
                // Return an unauthorized response.
                return StatusCode((int)HttpStatusCode.Unauthorized);
            }

            // Connect the user to the channel of the gift list specified
            try
            {
                var listId = connectionData.GiftListId.ToString();

                await _hubContext.Groups.AddToGroupAsync(connectionData.ConnectionId, listId);
                return StatusCode((int)HttpStatusCode.OK, "Connected to List Chat Channel");
            }catch(Exception e)
            {
                // TODO: Add logging
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize]
        [Route("api/LeaveListChat")]
        [HttpPost]
        public async Task<ActionResult> LeaveListChatChannel(ItemChatConnectDTO connectionData)
        {
            try
            {
                await _hubContext.Groups.RemoveFromGroupAsync(connectionData.ConnectionId, connectionData.GiftListId.ToString());
                return StatusCode((int)HttpStatusCode.OK, "Disconnected from list chat channel");
            }catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize]
        [Route("api/SendMessageToList")]
        [HttpPost]
        public async Task<ActionResult> SendMessageToList(string Message, int GiftListId)
        {
            string userId = await _userService.GetUserIdAsync();
            var userName = User.Claims.First(e => e.Type == "name").Value;
            var listId = GiftListId.ToString();

            var message = new ChatMessageDTO()
            {
                FromUser = userName,
                Message = Message,
            };

            var listMessage = new ListMessages()
            {
                UserId = userId,
                UserName = userName,
                CreatedAt = DateTime.Now,
                Message = Message,
                GiftListId = GiftListId
            };

            await _hubContext.Clients.Group(listId).SendAsync("ListMessage", message);

            await SaveMessageToDatabase(listMessage);

            return StatusCode((int)HttpStatusCode.OK);
        }

        [Authorize]
        [Route("api/ItemChat/getListMessages")]
        [HttpGet]
        public async Task<ActionResult> GetListMessages(int giftListId, int pageSize)
        {
            var messages = await _unitOfWork.ListMessages.GetPagedListMessages(giftListId, pageSize);

            return StatusCode((int)HttpStatusCode.OK, messages);
        }

        private async Task SaveMessageToDatabase(ListMessages listMessage)
        {
            _unitOfWork.ListMessages.Add(listMessage);

            await _unitOfWork.CompleteAsync();
            return;
        }
    }
}
