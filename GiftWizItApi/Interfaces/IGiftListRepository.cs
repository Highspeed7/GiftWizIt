using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Extensions;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IGiftListRepository : IRepository<GiftLists>
    {
        GiftLists Add(GiftListDto glist);
        Task<IEnumerable<GiftLists>> GetUserLists(string userId);
        PagedResult<GiftLists> GetGiftListsBySearch(string term, Page pager, string userId = null);
        Task<GiftLists> GetUserGiftListByIdAsync(string userId, int listId);
        Task DeleteGiftList(int listId);
    }
}
