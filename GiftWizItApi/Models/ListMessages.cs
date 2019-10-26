using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class ListMessages
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GiftListId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public Users User { get; set; }
        public GiftLists GiftList { get; set; }
    }
}
