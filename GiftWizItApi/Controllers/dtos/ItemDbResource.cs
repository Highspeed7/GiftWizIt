using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class ItemDbResource
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public List<LnksItmsPtnrsDTO> LinkItemPartners { get; set; }
    }
}
