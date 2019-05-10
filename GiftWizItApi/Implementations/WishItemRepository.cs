using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
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

        public WishItem Add(string userId, string listName, Items item, WishLists wList = null)
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
                Image = item.Image
            };

            var wishItem = new WishItem
            {
                WishList = wishList,
                Item = newItem
            };
            base.Add(wishItem);
            return wishItem;
        }

        public async Task<IEnumerable<WishItem>> GetWishItem(string userId)
        {
            var result = await Context.WishItems
                            .Include(wi => wi.WishList)
                            .Include(wi => wi.Item)
                            .Where(wi => wi.WishList.UserId == userId).ToListAsync();

            return result;
        }
    }
}
