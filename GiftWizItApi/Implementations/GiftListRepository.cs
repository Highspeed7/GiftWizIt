using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class GiftListRepository : Repository<GiftLists>, IGiftListRepository
    {
        public GiftListRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<IEnumerable<GiftLists>> GetUserListsAsync()
        //{
            
        //}

        public GiftLists Add(GiftListDto glist)
        {
            var giftList = new GiftLists
            {
                Name = glist.Name,
                UserId = glist.UserId
            };

            base.Add(giftList);
            return giftList;
        }

        public async Task<IEnumerable<GiftLists>> GetUserLists(string userId)
        {
            return await Context.GiftLists.Where(gl => gl.UserId == userId && gl.Deleted == false).ToListAsync();
        }

        public async Task DeleteGiftList(int listId)
        {
            var list = await Context.GiftLists.FirstAsync(gl => gl.Id == listId);
            list.Deleted = true;
        }
    }
}
