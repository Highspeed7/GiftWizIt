using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class UserCheckout
    {
        public string UserId { get; set; }
        public string CheckoutId { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string WebUrl { get; set; }

        public Users User { get; set; }
    }
}
