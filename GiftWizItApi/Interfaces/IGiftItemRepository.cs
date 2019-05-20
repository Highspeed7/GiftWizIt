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
        Task<GiftItem> GetGiftItemByIdAsync(int item_id);
        Task<CombGiftItems> GetGiftItemDetailsByIdAsync(int gift_list_id, string user_id, int itm_id);
    }
}
