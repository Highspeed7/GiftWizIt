using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftWizItApi.Interfaces;
using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;

namespace GiftWizItApi.Services
{
    public class AWSPAAPIService: IAWSPAAPIService
    {
        public AmazonAuthentication auth = new AmazonAuthentication("AKIAI4MIP4XCSON4STXQ", "byZ5O2/J1ynbmPZVb9q84HsiJIfUsl1xBJKrFCE0");
        public AmazonProductAdvertisingClient client;

        public AWSPAAPIService()
        {
            client = new AmazonProductAdvertisingClient(auth, AmazonEndpoint.US, "giftwizit19-20");
        }

        public async Task<SearchItemResponse> ItemSearch(string keywords, int page)
        {
            var searchRequest = new SearchRequest
            {
                Keywords = keywords,
                ItemPage = page,
                Resources = new[]
                {
                    "Images.Primary.Large",
                    "ItemInfo.Title",
                    "ItemInfo.Features"
                }
            };

            return await client.SearchItemsAsync(searchRequest);
        }
    }
}
