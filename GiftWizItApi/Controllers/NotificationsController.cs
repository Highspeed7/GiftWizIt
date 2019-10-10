using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
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
        // TODO: Work updating notification count into signalr push notifications somehow.

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService userService;
        private readonly INotificationsService notifService;
        private readonly IHubContext<MainHub> _hubContext;

        public NotificationsController(
            IUnitOfWork unitOfWork,
            IUserService userService,
            INotificationsService notifService,
            IHubContext<MainHub> hubContext
        )
        {
            _unitOfWork = unitOfWork;
            this.userService = userService;
            this._hubContext = hubContext;
            this.notifService = notifService;
        }

        [Route("api/NotificationsInit")]
        [HttpGet]
        public bool InitiateNotifications()
        {
            notifService.UserEmail = User.Claims.First(c => c.Type == "emails").Value;
            return true;
        }

        [Route("api/NotificationsCount")]
        [HttpGet]
        public async Task<ActionResult> GetUserNotificationsCount()
        {
            string userId = await userService.GetUserIdAsync();
            int notificiationsCount = await _unitOfWork.Notifications.GetNotificationsCount(userId);

            return StatusCode((int)HttpStatusCode.OK, notificiationsCount);
        }

        [Route("api/UserNotifications")]
        [HttpGet]
        public async Task<ActionResult> GetUserNotifications([FromQuery]Page pager = null)
        {
            string userId = await userService.GetUserIdAsync();

            var notifications = await _unitOfWork.Notifications.GetUserPagedNotificationsAsync(userId, pager);

            return StatusCode((int)HttpStatusCode.OK, notifications);
        }

        [Route("api/NotificationsChannel")]
        [HttpPost]
        public async Task<ActionResult> ConnectToNotificationsChannel(string connectionId)
        {
            string userId = await userService.GetUserIdAsync();
            this.notifService.UserEmail = User.Claims.First(c => c.Type == "emails").Value;
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