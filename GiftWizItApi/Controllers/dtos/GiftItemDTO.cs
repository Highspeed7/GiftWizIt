using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class GiftItemDTO
    {
        public int GListId { get; set; }
        public int Item_Id { get; set; }
        public bool Deleted { get; set; }
        public ItemDbResource Item { get; set; }
    }
}
