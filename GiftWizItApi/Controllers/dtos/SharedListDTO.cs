﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class SharedListDTO
    {
        public String Password { get; set; }
        public int GiftListId { get; set; }
        public string UserId { get; set; }
        public bool EmailSent { get; set; }
    }
}
