using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class ItemsRepository : Repository<Items>, IItemsRepository
    {
        public ItemsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public new Items Add(Items item)
        {
            base.Add(item);
            return item;
        }

        public async Task<Items> GetById(int itemId)
        {
            return await Context.Items.Where(i => i.Item_Id == itemId).FirstOrDefaultAsync();
        }
    }
}
