﻿using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class WishListRepository : Repository<WishLists>, IWishListRepository
    {
        public WishListRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<WishLists> GetWishListAsync(string listName, string userId)
        {
            return await Context.WishLists
                .Include(wl => wl.WishItems)
                .ThenInclude(wi => wi.Item)
                .Where(wl => wl.Name == listName && wl.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
