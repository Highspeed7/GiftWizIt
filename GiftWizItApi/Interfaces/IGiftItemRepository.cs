using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IGiftItemRepository: IRepository<GiftItem>
    {
        Task<IEnumerable<CombGiftItems>> GetGiftListItems(int gift_list_id, string user_id);
    }
}
