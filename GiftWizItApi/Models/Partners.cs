using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Partners
    {
        public int PartnerId { get; set; }
        public string Name { get; set; }

        public List<LnksItmsPtnrs> LinkItemPartners { get; set; }
    }
}
