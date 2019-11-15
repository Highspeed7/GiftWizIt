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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TagsController(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Route("GetTags")]
        [HttpGet]
        public async Task<ActionResult> GetAllTags()
        {
            var result = await unitOfWork.Tags.GetAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [Authorize]
        [Route("SetTags")]
        [HttpPost]
        public async Task<ActionResult> SetTags(TagsDTO[] tags)
        {
            Tags tagData = new Tags();
            foreach(TagsDTO tag in tags)
            {
                var newTag = new Tags();

                mapper.Map(tag, newTag);

               tagData = unitOfWork.Tags.Add(newTag);
            }

            var result = await unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }
    }
}