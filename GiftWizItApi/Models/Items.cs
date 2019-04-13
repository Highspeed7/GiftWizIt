using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Items
    {
        [Column("item_id")]
        public int Item_Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("upc")]
        public string UPC { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
    }
}
