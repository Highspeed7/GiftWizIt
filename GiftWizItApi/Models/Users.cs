using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Users
    {
        public string UserId { get; set; }

        public List<ContactUsers> ContactUsers { get; set; }

        public List<GiftLists> GiftLists { get; set; }
    }
}
