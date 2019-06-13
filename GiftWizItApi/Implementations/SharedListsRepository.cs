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
    public class SharedListsRepository : Repository<SharedLists>, ISharedListsRepository
    {
        public SharedListsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public SharedLists AddSharedList(SharedLists sharedList)
        {
            base.Add(sharedList);

            // I think this will return the giftlist associated with the share.
            Context.Entry(sharedList).Reference(sl => sl.GiftList).Load();

            return sharedList;
        }

        public async Task<SharedLists> GetSharedList(int giftListId, string giftListPass)
        {
            return await Context.SharedLists
                .Include(sl => sl.GiftList)
                .ThenInclude(gl => gl.GiftItems)
                .ThenInclude(gi => gi.Item)
                .ThenInclude(i => i.LinkItemPartners)
                .Where(sl => sl.Password == giftListPass && sl.GiftListId == giftListId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SharedLists>> GetAllUserSharedLists(string userId)
        {
            return await Context.SharedLists.Include(sl => sl.Contact).Include(sl => sl.GiftList).Where(sl => sl.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<SharedLists>>GetUserSharedListCollection(string userId, int giftListId)
        {
            return await Context.SharedLists.Include(sl => sl.Contact).Where(sl => sl.GiftListId == giftListId && sl.UserId == userId).ToListAsync();
        }
    }
}
