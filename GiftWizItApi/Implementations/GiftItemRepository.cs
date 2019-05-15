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
        public GiftItem Add(string userId, string listName, Items item, GiftLists giftList = null)
        {
            throw new NotImplementedException();
        }
    }
}
