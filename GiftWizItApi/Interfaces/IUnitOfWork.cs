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
        Task<int> CompleteAsync();
    }
}
