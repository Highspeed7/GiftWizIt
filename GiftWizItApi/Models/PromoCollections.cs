﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class PromoCollections
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string MatchTags { get; set; }

        public List<PromoItems> PromoItems { get; set; }

        public PromoCollections()
        {
            Start_Date = DateTime.Now;
            End_Date = DateTime.Now.AddYears(50);
        }
    }
}
