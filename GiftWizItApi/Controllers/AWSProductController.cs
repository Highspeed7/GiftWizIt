using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GiftWizItApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    [Route("api/AWS")]
    [ApiController]
    public class AWSProductController : ControllerBase
    {
        private readonly IAWSPAAPIService paapiService;

        public AWSProductController(
            IAWSPAAPIService paapiService
        )
        {
            this.paapiService = paapiService;
        }

        [Route("ItemSearch")]
        [HttpGet]
        public async Task<Object> ItemSearch(string keywords, int page)
        {
            var results = await paapiService.ItemSearch(keywords, page);

            return StatusCode((int)HttpStatusCode.OK, results);
        }
    }

    //internal class AWSItemSearch
    //{
    //}
}