using GiftWizItApi.Interfaces;
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
    }
}
