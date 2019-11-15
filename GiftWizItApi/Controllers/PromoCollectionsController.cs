using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Extensions;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GiftWizItApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCollectionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        
        public PromoCollectionsController(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Route("getPromoCollections")]
        [HttpGet]
        public async Task<ActionResult> GetAllPromoCollections()
        {
            var results = await unitOfWork.PromoCollections.GetAsync();

            return StatusCode((int)HttpStatusCode.OK, results);
        }

        [Authorize]
        [Route("setPromoCollections")]
        [HttpPost]
        public async Task<ActionResult> SetPromoCollections(PromoCollectionsDTO[] collections)
        {
            var email = User.Claims.First(e => e.Type == "emails").Value;

            // TODO: Replace with User Roles later
            if(email != "b.sanders7777@edmail.edcc.edu")
            {
                return StatusCode((int)HttpStatusCode.Unauthorized);
            }

            foreach(PromoCollectionsDTO collection in collections)
            {
                var newCollection = new PromoCollections()
                {
                    Name = collection.Name
                };

                unitOfWork.PromoCollections.Add(newCollection);
            }

            var result = await unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        
        [Route("GetPromoCollectionItems")]
        [HttpGet]
        public async Task<ActionResult> GetPromoCollectionItems(int collectionId)
        {
            var collection = await unitOfWork.PromoCollections.GetByIdAsync(collectionId);

            List<TagsDTO> tags = JsonConvert.DeserializeObject<List<TagsDTO>>(collection.MatchTags);

            List<ItemDTO> items = new List<ItemDTO>();

            foreach (TagsDTO tag in tags)
            {
                var dbItems = await unitOfWork.ItemTags.GetItemsWithTagAsync(tag.TagName);

                foreach (ItemTags dbItem in dbItems)
                {
                    ItemDTO item = new ItemDTO();

                    mapper.Map(dbItem.Item, item);

                    items.Add(item);
                }
            }

            items = items.DistinctBy(i => i.Item_Id).ToList();

            return StatusCode((int)HttpStatusCode.OK, items);
        }
    }
}