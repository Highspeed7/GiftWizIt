using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class QueryGiftItemDTO
    {
        public int Item_Id { get; set; }
        public int Gift_List_Id { get; set; }
        public int Partner_Id { get; set; }
        public string Afflt_Link { get; set; }
        public string Product_Id { get; set; }
        public string Itm_Name { get; set; }
        public string Glst_Name { get; set; }
        public string Image { get; set; }
        public string ClaimedById { get; set; }
        public string ClaimedBy { get; set; }
    }
}
