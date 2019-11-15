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
            CreateMap<Contacts, ContactDTO>()
                .ForMember(d => d.Alias, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ContactUsersDTO, ContactUsers>();
            CreateMap<SharedListDTO, SharedLists>();
            CreateMap<SharedLists, SharedFromDTO>()
                .ForMember(sf => sf.GiftListName, opt => opt.MapFrom(sl => sl.GiftList.Name))
                .ForMember(sf => sf.IsPublic, opt => opt.MapFrom(sl => sl.GiftList.IsPublic))
                .ForMember(sf => sf.FromUser, opt => opt.MapFrom(sl => sl.User.Name))
                .ReverseMap();

            CreateMap<Items, ItemDTO>().ReverseMap();
            CreateMap<LnksItmsPtnrsDTO, LnksItmsPtnrs>();
            CreateMap<GiftListDto, GiftLists>();
            CreateMap<GiftItemDTO, GiftItem>();
            CreateMap<ItemDbResource, Items>();
            CreateMap<TagsDTO, Tags>();
            CreateMap<PageRes, Page>();
        }
    }
}
