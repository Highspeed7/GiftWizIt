using GiftWizItApi.Extensions;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class NotificationsRepository : Repository<Notifications>, INotificationsRepository
    {
        public NotificationsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetNotificationsCount(string userId)
        {
            return await Context.Notifications.Where(n => n.UserId == userId && 
                                n.Deleted == false && 
                                n.Dismissed == false).CountAsync();
        }

        public async Task<PagedResult<Notifications>> GetUserPagedNotificationsAsync(string userId, Page pager)
        {
            var notifications = Context.Notifications.Where(n => n.UserId == userId && n.Deleted == false && n.Dismissed == false).OrderByDescending(n => n.CreatedOn);

            return await notifications.GetPaged(pager.PageCount, pager.PageSize);
        }
    }
}
