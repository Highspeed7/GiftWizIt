using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class LnksItmsPtnrs
    {
        public string AffliateLink { get; set; }
        public int ItemId { get; set; }
        public Items Item { get; set; }
        public int PartnerId { get; set; }
        public Partners Partner { get; set; }
    }
}
