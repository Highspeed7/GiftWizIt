using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class ItemTags
    {
        public int TagId { get; set; }
        public int ItemId { get; set; }

        public Tags Tag { get; set; }
        public Items Item { get; set; }
    }
}
