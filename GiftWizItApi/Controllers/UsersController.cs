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
        public async Task<int> RegisterUserId([FromBody] Users userId)
        {
            var user = await this._unitOfWork.Users.GetUserByIdAsync(userId.UserId);
            if(user == null)
            {
                _unitOfWork.Users.Add(userId);
            }
            
            return await _unitOfWork.CompleteAsync();
        }
    }
}