﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Users
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public List<ContactUsers> ContactUsers { get; set; }

        public List<UserCheckout> UserCheckouts { get; set; }

        public List<GiftLists> GiftLists { get; set; }

        public List<ListMessages> ListMessages { get; set; }

        public List<WishLists> WishLists { get; set; }

        public List<SharedLists> SharedLists { get; set; }

        public List<Notifications> Notifications { get; set; }

        public List<ItemClaims> ItemClaims { get; set; }

        public UserFacebook UserFacebook { get; set; }
    }
}
