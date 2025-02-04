﻿using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface ISharedListsRepository: IRepository<SharedLists>
    {
        SharedLists AddSharedList(SharedLists sharedList);
        Task<SharedLists> GetSharedList(int giftListId, string giftListPass);
        Task<IEnumerable<SharedLists>> GetAllUserSharedLists(string userId);
        Task<IEnumerable<SharedLists>> GetUserSharedListCollection(string userId, int giftListId);
        Task<IEnumerable<SharedLists>> GetListsByContactId(int contactId);
        Task<IEnumerable<SharedLists>> GetEditableListsByContactId(int contactId);
    }
}
