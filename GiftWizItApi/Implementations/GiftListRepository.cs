using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class GiftListRepository : Repository<GiftLists>, IGiftListRepository
    {
        public GiftListRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<IEnumerable<GiftLists>> GetUserListsAsync()
        //{
            
        //}
    }
}
