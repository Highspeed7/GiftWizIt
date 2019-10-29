using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Tags
    {
        public int Id { get; set; }
        public int TagName { get; set; }
        public bool Deleted { get; set; }

        public List<ItemTags> ItemTags { get; set; }
    }
}
