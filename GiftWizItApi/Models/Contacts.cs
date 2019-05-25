using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Contacts
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }
        public bool EmailSent { get; set; }
        public string VerifyGuid { get; set; }

        public List<ContactUsers> ContactUsers { get; set; }
    }
}
