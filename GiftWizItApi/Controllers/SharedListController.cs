using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [ApiController]
    public class SharedListController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public SharedListController(
            IUnitOfWork unitOfWork,
            IUserService userService,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.mapper = mapper;
        }

        [Authorize]
        [Route("api/SharedList/Contacts")]
        [HttpGet]
        public async Task<ActionResult> GetSharedListContacts()
        {
            var userId = await userService.GetUserIdAsync();

            var result = await unitOfWork.SharedLists.GetAllUserSharedLists(userId);

            List<SharedListDTO> sharedList = mapper.Map<IEnumerable<SharedListDTO>>(result).ToList();

            return StatusCode((int)HttpStatusCode.OK, sharedList);
        }

        [Route("api/SharedList")]
        [HttpPost]
        public async Task<ActionResult> GetSharedList(SharedListResource sharedList)
        {
            var result = await unitOfWork.SharedLists.GetSharedList(sharedList.GListId, sharedList.GListPass);

            // Validate the giftlistid

            // Check to see if the list has already been shared and set the same password.

            if(result == null)
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }else
            {
                SharedListDTO sharedListResource = new SharedListDTO()
                {
                    GiftList = mapper.Map<GiftListDto>(result.GiftList)
                };

                return StatusCode((int)HttpStatusCode.OK, sharedListResource);
            }
        }
    }
}