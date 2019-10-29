using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class PromoItems
    {
        public int ItemId { get; set; }
        public int CollectionId { get; set; }

        public PromoCollections Collection { get; set; }
        public Items Item { get; set; }
    }
}
