using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class ItemDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public string Image { get; set; }

        public List<LnksItmsPtnrsDTO> LinkItemPartners { get; set; }
    }
}
