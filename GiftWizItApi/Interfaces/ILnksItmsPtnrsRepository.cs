using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface ILnksItmsPtnrsRepository: IRepository<LnksItmsPtnrs>
    {
        void Add(string url, int item_id, int partner_id);
        Task<LnksItmsPtnrs> GetLnksItmsPtnrs(int partnerId);
        Task<LnksItmsPtnrs> GetItemByDomainAsync(string itemUrl);
        Task<IEnumerable<LnksItmsPtnrs>> PerformLinkQuery();
    }
}
