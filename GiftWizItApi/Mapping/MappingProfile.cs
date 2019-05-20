using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemDTO, Items>()
                .ForSourceMember(source => source.Url, opt => opt.DoNotValidate())
                .ForSourceMember(source => source.Domain, opt => opt.DoNotValidate());
            CreateMap<WishListRaw, WishListDto>();
            CreateMap<CombGiftItems, QueryGiftItemDTO>();
            CreateMap<GiftItemDTO, GiftItem>()
                .ForMember(d => d.GListId, opt => opt.MapFrom(s => s.To_Glist_Id));
        }
    }
}
