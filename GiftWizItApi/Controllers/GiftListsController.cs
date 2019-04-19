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
        public async Task<int> CreateList(GiftLists glist)
        {
            _unitOfWork.GiftLists.Add(glist);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
