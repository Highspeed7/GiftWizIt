using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IItemClaimsRepository: IRepository<ItemClaims>
    {
        Task<ItemClaims> GetItemClaim(int item_id, int list_id);
    }
}
