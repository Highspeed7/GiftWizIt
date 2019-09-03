using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GiftWizItApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [Authorize]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService userService;

        public NotificationsController(
            IUnitOfWork unitOfWork,
            IUserService userService
        )
        {
            _unitOfWork = unitOfWork;
            this.userService = userService;
        }

        [Route("api/NotificationsCount")]
        [HttpGet]
        public async Task<ActionResult> GetUserNotificationsCount()
        {
            string userId = await userService.GetUserIdAsync();
            int notificiationsCount = await _unitOfWork.Notifications.GetNotificationsCount(userId);

            return StatusCode((int)HttpStatusCode.OK, notificiationsCount);
        }
    }
}