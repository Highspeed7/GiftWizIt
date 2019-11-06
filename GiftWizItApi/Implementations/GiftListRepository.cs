using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using GiftWizItApi.Extensions;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace GiftWizItApi.Implementations
{
    public class GiftListRepository : Repository<GiftLists>, IGiftListRepository
    {
        public GiftListRepository(ApplicationDbContext context) : base(context)
        {
        }

        public GiftLists Add(GiftListDto glist)
        {
            var giftList = new GiftLists
            {
                Name = glist.Name,
                Password = glist.Password,
                RestrictChat = glist.RestrictChat,
                AllowItemAdds = glist.AllowItemAdds,
                IsPublic = glist.IsPublic,
                UserId = glist.UserId
            };

            base.Add(giftList);
            return giftList;
        }

        public async Task<IEnumerable<GiftLists>> GetUserLists(string userId)
        {
            return await Context.GiftLists
                .Where(gl => gl.UserId == userId && gl.Deleted == false)
                .IncludeFilter(gl => gl.GiftItems.Where(gi => gi.Deleted == false))
                .ToListAsync();
        }

        public async Task<PagedResult<GiftLists>> GetPrivateGiftListsBySearch(string term, Page pager, string password, string userId)
        {
            var giftLists = Context.GiftLists
                .Where(gl => gl.Name.Contains(term)
                    && gl.IsPublic == false
                    && gl.Deleted == false
                    && gl.Password == password
                    && gl.UserId == userId
                );

            return await giftLists.GetPaged(pager.PageCount, pager.PageSize);
        }

        public async Task<PagedResult<GiftLists>> GetPublicGiftListsBySearch(string term, Page pager, string userId = null)
        {
            var giftLists = Context.GiftLists.Where(gl => gl.Name.Contains(term) && gl.IsPublic == true && gl.Deleted == false);

            if(userId != null)
            {
                giftLists.Where(gl => gl.UserId == userId);
            }

            return await giftLists.GetPaged(pager.PageCount, pager.PageSize);
        }

        public async Task<GiftLists> GetUserGiftListByIdAsync(string userId, int listId)
        {
            return await Context.GiftLists.Where(gl => gl.UserId == userId && gl.Id == listId).FirstOrDefaultAsync();
        }

        public async Task DeleteGiftList(int listId)
        {
            var list = await Context.GiftLists.FirstAsync(gl => gl.Id == listId);
            list.Deleted = true;
        }
    }
}
