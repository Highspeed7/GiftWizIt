﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Notifications
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public bool Dismissed { get; set; }
        public bool Persist { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public Users User { get; set; }
    }
}
