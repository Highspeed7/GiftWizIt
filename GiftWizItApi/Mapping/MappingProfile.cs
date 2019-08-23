using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Models;

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
            CreateMap<GiftItemMoveDTO, GiftItem>()
                .ForMember(d => d.GListId, opt => opt.MapFrom(s => s.To_Glist_Id));
            CreateMap<ContactDTO, Contacts>();
            CreateMap<ContactUsersDTO, ContactUsers>();
            CreateMap<SharedListDTO, SharedLists>();
            CreateMap<LnksItmsPtnrsDTO, LnksItmsPtnrs>();
            CreateMap<GiftListDto, GiftLists>();
            CreateMap<GiftItemDTO, GiftItem>();
            CreateMap<ItemDbResource, Items>();
            CreateMap<PageRes, Page>();
        }
    }
}
