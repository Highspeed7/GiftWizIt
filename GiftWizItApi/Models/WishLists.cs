﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class WishLists
    {
        [Column("wish_list_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("created_on")]
        public DateTime CreatedAt { get; set; }

        public List<WishItem> WishItems { get; set; }
    }
}
