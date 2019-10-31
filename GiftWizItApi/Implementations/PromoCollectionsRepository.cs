using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class PromoCollectionsRepository : Repository<PromoCollections>, IPromoCollectionsRepository
    {
        public PromoCollectionsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PromoCollections> GetByIdAsync(int collectionId)
        {
            return await Context.PromoCollections.Where(pc => pc.Id == collectionId).FirstOrDefaultAsync();
        }
    }
}
