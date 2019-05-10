using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IWishItemRepository : IRepository<WishItem>
    {
        WishItem Add(string userId, string listName, Items item, WishLists wishList = null);
        Task<IEnumerable<WishItem>> GetWishItem(string userId);
    }
}
