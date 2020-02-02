using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nager.AmazonProductAdvertising.Model;

namespace GiftWizItApi.Interfaces
{
    public interface IAWSPAAPIService
    {
        Task<SearchItemResponse> ItemSearch(string keywords, int page);
    }
}
