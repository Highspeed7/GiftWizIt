using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class GiftItemRepository : Repository<GiftItem>, IGiftItemRepository
    {
        public GiftItemRepository(ApplicationDbContext context): base(context)
        {

        }
    }
}
