using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers.dtos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoItemsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PromoItemsController(
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [Authorize]
        [Route("SetPromoItems")]
        public async Task<ActionResult> SetPromoItems(PromoItemsDTO[] promoItems)
        {
            // TODO: Add check later for items already added to the promo-items table
            foreach(PromoItemsDTO item in promoItems)
            {
                var partners = await unitOfWork.Partners.GetPartnerAsync(item.Domain);
                var partner = partners.FirstOrDefault();

                if(partner == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid Domain");
                }
                else
                {
                    var newItem = new Items();

                    // Using ItemDTO because a mapping already exists for it.
                    var itemToMap = new ItemDTO()
                    {
                        Name = item.Name,
                        Url = item.Url,
                        Domain = item.Domain,
                        Image = item.Image,
                        ProductId = item.ProductId
                    };

                    mapper.Map(itemToMap, newItem);

                    var insertedItem = unitOfWork.Items.Add(newItem);

                    var result = await unitOfWork.CompleteAsync();
                    if(result > 0)
                    {
                        try
                        {
                            unitOfWork.LnksItmsPtns.Add(item.Url, insertedItem.Item_Id, partner.PartnerId);

                            if (item.Tags.Count() > 0)
                            {
                                var tags = await unitOfWork.Tags.GetAsync();

                                foreach (TagsDTO tag in item.Tags)
                                {
                                    var dbTag = tags.Where(t => t.TagName == tag.TagName).FirstOrDefault();

                                    if (dbTag == null)
                                    {
                                        Tags insertedTag = new Tags();

                                        // Add the new tag
                                        insertedTag = unitOfWork.Tags.Add(new Tags()
                                        {
                                            TagName = tag.TagName
                                        });

                                        // Add the item-tag association
                                        unitOfWork.ItemTags.Add(new ItemTags()
                                        {
                                            ItemId = insertedItem.Item_Id,
                                            TagId = insertedTag.Id
                                        });
                                    }
                                    else
                                    {
                                        unitOfWork.ItemTags.Add(new ItemTags()
                                        {
                                            ItemId = insertedItem.Item_Id,
                                            TagId = dbTag.Id
                                        });
                                    }
                                }
                            } else
                            {
                                return StatusCode((int)HttpStatusCode.BadRequest, "No tags provided");
                            }

                            // Track the promotional items
                            unitOfWork.PromoItems.Add(new PromoItems()
                            {
                                ItemId = insertedItem.Item_Id
                            });

                            await unitOfWork.CompleteAsync();
                        } catch (Exception e)
                        {
                            return StatusCode((int)HttpStatusCode.InternalServerError, $"Save not successful; the item inserted was {insertedItem.Item_Id} before execution was terminated.");
                        }
                    }else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, "Insertion of item failed");
                    }
                }
            }
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}