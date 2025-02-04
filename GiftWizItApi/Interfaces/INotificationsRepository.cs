﻿using GiftWizItApi.Extensions;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface INotificationsRepository: IRepository<Notifications>
    {
        Task<int> GetNotificationsCount(string userId);
        Task<PagedResult<Notifications>> GetUserPagedNotificationsAsync(string userId, Page pager);
    }
}
