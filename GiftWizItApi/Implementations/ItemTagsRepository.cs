using GiftWizItApi.Extensions;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace GiftWizItApi.Implementations
{
    public class ItemTagsRepository : Repository<ItemTags>, IItemTagsRepository
    {
        public ItemTagsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ItemTags>> GetItemsWithTagAsync(string tag)
        {
            var result = await Context.ItemTags
                                    .Include(it => it.Item)
                                    .ThenInclude(i => i.LinkItemPartners)
                                    .Include(it => it.Tag)
                                    .Where(it => it.Tag.TagName == tag && it.Deleted == false).ToListAsync();
            return result;
        }
    }
}
