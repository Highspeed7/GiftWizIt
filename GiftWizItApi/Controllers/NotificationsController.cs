using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GiftWizItApi.Interfaces;
using GiftWizItApi.SignalR.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GiftWizItApi.Controllers
{
    [Authorize]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService userService;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public NotificationsController(
            IUnitOfWork unitOfWork,
            IUserService userService,
            IHubContext<NotificationsHub> hubContext
        )
        {
            _unitOfWork = unitOfWork;
            this.userService = userService;
            this._hubContext = hubContext;
        }

        [Route("api/NotificationsCount")]
        [HttpGet]
        public async Task<ActionResult> GetUserNotificationsCount()
        {
            string userId = await userService.GetUserIdAsync();
            int notificiationsCount = await _unitOfWork.Notifications.GetNotificationsCount(userId);

            return StatusCode((int)HttpStatusCode.OK, notificiationsCount);
        }

        [Route("api/NotificationsChannel")]
        [HttpPost]
        public async Task<ActionResult> ConnectToNotificationsChannel(string connectionId)
        {
            string userId = await userService.GetUserIdAsync();
            try
            {
                await _hubContext.Groups.AddToGroupAsync(connectionId, userId);
                return StatusCode((int)HttpStatusCode.OK, "Connected to Notifications");
            }catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Could not connect to group");
            }
        }
    }
}