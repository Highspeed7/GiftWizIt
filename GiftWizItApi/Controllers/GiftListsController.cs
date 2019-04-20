using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers
{
    [Authorize]
    [ApiController]
    public class GiftListsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GiftListsController(IGiftListRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("api/GiftLists/")]
        [HttpPost]
        public async Task<int> CreateList(GiftListDto glist)
        {
            // Check for user in database
            var user = await _unitOfWork.Users.GetUserByIdAsync(glist.UserId);
            if (user == null)
            {
                var newUser = new Users
                {
                    UserId = glist.UserId
                };
                _unitOfWork.Users.Add(newUser);
            }
            else
            {
                _unitOfWork.GiftLists.Add(glist);
            }

            return await _unitOfWork.CompleteAsync();
        }

        [Route("api/GiftLists/")]
        [HttpGet]
        public async Task<IEnumerable<GiftLists>> GetUserGiftLists(string userId)
        {
            return await _unitOfWork.GiftLists.GetUserLists(userId);
        }
    }
}
