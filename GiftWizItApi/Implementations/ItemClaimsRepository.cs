using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class ItemClaimsRepository : Repository<ItemClaims>, IItemClaimsRepository
    {
        public ItemClaimsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
