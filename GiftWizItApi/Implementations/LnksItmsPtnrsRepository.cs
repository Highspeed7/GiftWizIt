using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class LnksItmsPtnrsRepository : Repository<LnksItmsPtnrs>, ILnksItmsPtnrsRepository
    {
        public LnksItmsPtnrsRepository(ApplicationDbContext context): base(context)
        {
        }

        public void Add(string url, int item_id, int partner_id)
        {
            base.Add(new LnksItmsPtnrs
            {
                AffliateLink = url,
                ItemId = item_id,
                PartnerId = partner_id
            });
        }

        public Task<LnksItmsPtnrs> GetLnksItmsPtnrs(int partnerId)
        {
            throw new NotImplementedException();
        }

        public async Task<LnksItmsPtnrs> GetItemByDomainAsync(string itemUrl)
        {
            return await Context.LinkItemsPartners.Where(lip => lip.AffliateLink == itemUrl).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LnksItmsPtnrs>> PerformLinkQuery()
        {
            return await Context.LinkItemsPartners.FromSql($"SELECT * FROM Links_Items_Partners WHERE afflt_link LIKE '%amazon.com%'").ToListAsync();
        }
    }
}
