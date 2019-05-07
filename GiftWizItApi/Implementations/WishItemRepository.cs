using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class WishItemRepository : Repository<WishItem>, IWishItemRepository
    {
        public WishItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public WishItem Add(string userId, string listName, ItemDTO item, WishLists wList = null)
        {
            WishLists wishList = new WishLists();

            if(wList == null)
            {
                wishList = new WishLists
                {
                    Name = listName,
                    UserId = userId
                };
            }else
            {
                wishList = wList;
            }

            var newItem = new Items
            {
                Name = item.Name,
            };

            var wishItem = new WishItem
            {
                WishList = wishList,
                Item = newItem
            };
            base.Add(wishItem);
            return wishItem;
        }
    }
}
