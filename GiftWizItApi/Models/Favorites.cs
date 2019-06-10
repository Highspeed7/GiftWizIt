using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Favorites
    {
        public int Id { get; set; }
        public int G_List_Id { get; set; }
        public int Item_Id { get; set; }
        public int Contact_Id { get; set; }

        public Items Item { get; set; }
        public Contacts Contact { get; set; }
        public GiftLists GiftList { get; set; }
    }
}
