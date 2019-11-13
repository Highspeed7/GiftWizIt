using GiftWizItApi.Extensions;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IListMessagesRepository : IRepository<ListMessages>
    {
        Task<PagedResult<ListMessages>> GetPagedListMessages(int giftListId, int pageSize, int skipCount);
        Task<int> GetListMessagesCountAsync(int giftListId);
    }
}
