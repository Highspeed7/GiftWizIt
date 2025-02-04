﻿using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IWishListRepository : IRepository<WishLists>
    {
        Task<WishLists> GetWishListAsync(string listName, string userId);
    }
}
