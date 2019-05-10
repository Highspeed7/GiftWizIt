using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class WishListRaw
    {
        public int Item_Id { get; set; }
        public int W_List_Id { get; set; }
        public int Partner_Id { get; set; }
        public string Afflt_Link { get; set; }
        public string Itm_Name { get; set; }
        public string Wlst_Name { get; set; }
        public string Image { get; set; }
    }
}
