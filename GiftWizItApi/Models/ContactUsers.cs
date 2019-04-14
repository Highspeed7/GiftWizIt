using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class ContactUsers
    {
        public int ContactId { get; set; }
        public Contacts Contact { get; set; }

        public string UserId { get; set; }
        public Users User { get; set; }
    }
}
