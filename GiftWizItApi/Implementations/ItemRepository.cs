using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class ItemRepository : Repository<Items>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void Add(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
