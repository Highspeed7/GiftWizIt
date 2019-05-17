using AutoMapper;
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
        private readonly IMapper mapper;

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

        public async Task<IEnumerable<WishListRaw>> GetWishItem(string userId)
        {
            var result = await Context.DbWishListObject.FromSql($"SELECT wi.item_id, i.image, w_list_id, partner_id, Afflt_Link, i.name as itm_name, wshl.name as wlst_name FROM WList_Items as wi JOIN Links_Items_Partners as lip ON wi.w_list_id IN( SELECT wl.wish_list_id FROM WishLists as wl WHERE wl.UserId = {userId}) JOIN Items as i ON wi.item_id = i.item_id JOIN WishLists as wshl ON wshl.wish_list_id = wi.w_list_id WHERE lip.item_id = wi.item_id AND wi._deleted = 'false'").ToListAsync();
            
            return result;
        }

        public async Task<WishItem> GetWishItemByItemId(int itemId)
        {
            var result = await Context.WishItems.Where(wi => wi.ItemId == itemId && wi.Deleted != true).FirstAsync();
            return result;
        }
    }
}
