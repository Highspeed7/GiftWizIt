using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class SharedListDTO
    {
        public int GiftListId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        public GiftListDto GiftList { get; set; }
        public ContactDTO Contact { get; set; }
    }
}
