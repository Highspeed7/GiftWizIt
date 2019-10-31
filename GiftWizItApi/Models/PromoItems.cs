using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class PromoItems
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Items Item { get; set; }
    }
}
