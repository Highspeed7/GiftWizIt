using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class SharedFromDTO
    {
        public int GiftListId { get; set; }
        public string GiftListName { get; set; }
        public string IsPublic { get; set; }
        public string FromUser { get; set; }
    }
}
