using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class GiftItem
    {
        public int GListId { get; set; }
        public GiftLists GiftList { get; set; }
        public string Purchase_Status { get; set; }
        public bool Deleted { get; set; }

        public int Item_Id { get; set; }
        public Items Item { get; set; }
    }
}
