using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IItemsRepository : IRepository<Items>
    {
        void Add(string userId);
        Task<Items> GetById(int itemId);
    }
}
