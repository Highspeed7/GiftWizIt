using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService userService;

        public UsersController(
            IUserRepository respository,
            IUnitOfWork unitOfWork,
            IUserService userService
            )
        {
            _unitOfWork = unitOfWork;
            this.userService = userService;
        }

        [Route("api/UserData")]
        [HttpGet]
        public UserDataDTO GetUserData()
        {
            var email = User.Claims.First(e => e.Type == "emails").Value;
            var username = User.Claims.First(e => e.Type == "name").Value;

            var result = new UserDataDTO
            {
                Username = username,
                Email = email
            };

            return result;
        }

        [Route("api/Users")]
        [HttpPost]
        public async Task<int> RegisterUser()
        {
            var userId = await userService.GetUserIdAsync();

            var email = User.Claims.First(e => e.Type == "emails").Value;
            var name = User.Claims.First(e => e.Type == "name").Value;

            var user = await _unitOfWork.Users.GetUserByIdAsync(userId);

            // Check contacts to see if one needs to be associated with a user
            var contact = await _unitOfWork.Contacts.GetContactByEmail(email);

            if (user == null)
            {
               user = _unitOfWork.Users.Add(userId, email, name);
            }

            // If a contact was returned
            if (contact != null)
            {
                // If the userId isn't already set
                if (contact.UserId != user.UserId)
                {
                    // Associate the user with the contact.
                    contact.UserId = user.UserId;
                }
            }

            return await _unitOfWork.CompleteAsync();
        }
    }
}