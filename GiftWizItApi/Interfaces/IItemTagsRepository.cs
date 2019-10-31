using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IItemTagsRepository: IRepository<ItemTags>
    {
        Task<IEnumerable<ItemTags>> GetItemsWithTagAsync(string tag);
    }
}
