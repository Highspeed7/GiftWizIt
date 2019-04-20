using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IGiftListRepository : IRepository<GiftLists>
    {
        void Add(GiftListDto glist);
        Task<IEnumerable<GiftLists>> GetUserLists(string userId);
    }
}
