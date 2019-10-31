using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGiftListRepository GiftLists { get; }
        IGiftItemRepository GiftItems { get; }
        IUserRepository Users { get; }
        IWishItemRepository WishItems { get; }
        IWishListRepository WishLists { get; }
        ILnksItmsPtnrsRepository LnksItmsPtns { get; }
        IPartnersRepository Partners { get; }
        IContactRepository Contacts { get; }
        IContactUsersRepository ContactUsers { get; }
        ISharedListsRepository SharedLists { get; }
        IItemsRepository Items { get; }
        IUserFacebookRepository UserFacebook { get; }
        INotificationsRepository Notifications { get; }
        IUserCheckoutRepository UserCheckout { get; }
        IListMessagesRepository ListMessages { get; }
        IPromoCollectionsRepository PromoCollections { get; }
        IPromoItemsRepository PromoItems { get; }
        IItemTagsRepository ItemTags { get; }
        ITagsRepository Tags { get; }
        Task<int> CompleteAsync();
    }
}
