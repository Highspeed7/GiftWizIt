using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ItemClaims> GetItemClaim(int item_id, int list_id)
        {
            return await Context.ItemClaims.Where(ic => ic.GiftListId == list_id && ic.ItemId == item_id).FirstOrDefaultAsync();
        }
    }
}
