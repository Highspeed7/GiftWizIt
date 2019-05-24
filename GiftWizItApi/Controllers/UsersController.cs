using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUserRepository respository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("api/Users")]
        [HttpPost]
        public async Task<int> RegisterUser()
        {
            // TODO: Help mitigate incidents where facebook email is the same as an already registered user.
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var email = User.Claims.First(e => e.Type == "emails").Value;

            var user = await _unitOfWork.Users.GetUserByIdAsync(userId);

            if (user == null)
            {
                _unitOfWork.Users.Add(userId, email);
            }else
            {
                return 0;
            }

            return await _unitOfWork.CompleteAsync();
        }

        [Route("api/Users/GetUserId")]
        [HttpGet]
        public string GetUserId()
        {
            return User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
        }
    }
}