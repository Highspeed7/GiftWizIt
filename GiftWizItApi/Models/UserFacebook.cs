using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class UserFacebook
    {
        public string UserId { get; set; }
        public string FacebookId { get; set; }

        public Users User { get; set; }
    }
}
