using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string ProductId { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }

        public List<GiftItem> GiftItems { get; set; }
        public List<WishItem> WishItems { get; set; }
        public List<LnksItmsPtnrs> LinkItemPartners { get; set; }
        public List<Favorites> Favorites { get; set; }
        public List<PromoItems> PromoItems { get; set; }
        public List<ItemTags> Tags { get; set; }
        public List<ItemClaims> ItemClaims { get; set; }

        public Items()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
