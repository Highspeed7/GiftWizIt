using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class SharedLists
    {
        public string Password { get; set; }
        public bool EmailSent { get; set; }
        public int GiftListId { get; set; }
        public int ContactId { get; set; }
        public string UserId { get; set; }

        public GiftLists GiftList { get; set; }
        public Users User { get; set; }
        public Contacts Contact { get; set; }
    }
}
