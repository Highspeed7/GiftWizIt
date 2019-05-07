using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGiftListRepository GiftLists { get; }
        IWishListRepository WishLists { get; }
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}
