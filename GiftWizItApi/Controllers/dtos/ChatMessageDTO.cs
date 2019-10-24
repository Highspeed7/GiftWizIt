using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class ChatMessageDTO
    {
        public string FromUser { get; set; }
        public string Message { get; set; }
        //public DateTime CreatedOn { get; set; }
    }
}
