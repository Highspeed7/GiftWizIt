using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class PromoItemsDTO
    {
        public int Item_Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public string Image { get; set; }
        public string ProductId { get; set; }
        
        public TagsDTO[] Tags { get; set; }
    }
}
