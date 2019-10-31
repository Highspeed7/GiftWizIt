using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IItemsRepository : IRepository<Items>
    {
        new Items Add(Items item);
        Task<Items> GetById(int itemId);
    }
}
