using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class GiftItemMoveDTO
    {
        public int G_List_Id { get; set; }
        public int Item_Id { get; set; }
        public int? To_Glist_Id { get; set; }
        public ItemDTO Item { get; set; }
    }
}
