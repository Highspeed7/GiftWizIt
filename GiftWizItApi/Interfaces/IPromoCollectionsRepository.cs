using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IPromoCollectionsRepository: IRepository<PromoCollections>
    {
        Task<PromoCollections> GetByIdAsync(int collectionId);
    }
}
