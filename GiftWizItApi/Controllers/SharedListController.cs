using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [ApiController]
    public class SharedListController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SharedListController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Route("api/SharedList")]
        [HttpPost]
        public async Task<ActionResult> GetSharedList(SharedListResource sharedList)
        {
            var result = await unitOfWork.SharedLists.GetSharedList(sharedList.GListId, sharedList.GListPass);

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