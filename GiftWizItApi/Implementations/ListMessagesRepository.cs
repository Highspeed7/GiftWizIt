using GiftWizItApi.Extensions;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class ListMessagesRepository : Repository<ListMessages>, IListMessagesRepository
    {
        public ListMessagesRepository(ApplicationDbContext context) : base(context){}

        public async Task<PagedResult<ListMessages>> GetPagedListMessages(int giftListId, int pageSize)
        {
            Page pager = new Page()
            {
                PageSize = pageSize
            };

            var result = Context.ListMessages.Where(lm => lm.GiftListId == giftListId).OrderByDescending(lm => lm.CreatedAt);

            return await result.GetPaged(pager.PageCount, pager.PageSize);
        }
    }
}
