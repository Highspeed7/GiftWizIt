using GiftWizItApi.Controllers.dtos;
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
    }
}
