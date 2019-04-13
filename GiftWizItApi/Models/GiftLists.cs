using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class GiftLists
    {
        [Column("gift_list_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("created_on")]
        public DateTime CreatedAt { get; set; }
    }
}
