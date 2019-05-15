using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    interface IGiftItemRepository: IRepository<GiftItem>
    {
        GiftItem Add(string userId, string listName, Items item, GiftLists giftList = null);
    }
}
