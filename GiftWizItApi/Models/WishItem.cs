using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class WishItem
    {
        public int ItemId { get; set; }
        public Items Item { get; set; }

        public int WListId { get; set; }
        public WishLists WishList { get; set; }
    }
}
