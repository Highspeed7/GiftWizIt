using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class ItemClaims
    {
        public int ClaimId { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }
        public int GiftListId { get; set; }

        public Items Item { get; set; }
        public GiftLists GiftList { get; set; }
        public Users User { get; set; }
    }
}
