using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IUserService _userService;

        public CheckoutController(
            IUnitOfWork unitOfWork,
            IUserService userService
        )
        {
            _unitOfwork = unitOfWork;
            _userService = userService;
        }

        [Authorize]
        [Route("api/storeCheckout")]
        [HttpPost]
        public async Task<ActionResult> StoreUserCheckout(UserCheckoutDTO checkout)
        {
            var userId = await _userService.GetUserIdAsync();

            var userCheckout = new UserCheckout()
            {
                UserId = userId,
                CheckoutId = checkout.Id,
                DateCreated = DateTime.Now,
                WebUrl = checkout.WebUrl
            };

            _unitOfwork.UserCheckout.Add(userCheckout);

            var result = await _unitOfwork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        // TODO: Obscure return data
        [Authorize]
        [Route("api/getCheckout")]
        [HttpGet]
        public async Task<ActionResult> GetUserCheckout()
        {
            var userId = await _userService.GetUserIdAsync();
            var checkout = await _unitOfwork.UserCheckout.GetUserCheckout(userId);

            return StatusCode((int)HttpStatusCode.OK, checkout);
        }
    }
}