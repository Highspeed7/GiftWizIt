using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGiftListRepository GiftLists { get; }
        IUserRepository Users { get; }
        IWishItemRepository WishItems { get; }
        IWishListRepository WishLists { get; }
        ILnksItmsPtnrsRepository LnksItmsPtns { get; }
        IPartnersRepository Partners { get; }
        Task<int> CompleteAsync();
    }
}
