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

        public string UserId { get; set; }
        public Users Users { get; set; }

        public string Password { get; set; }

        public bool IsPublic { get; set; }

        public bool RestrictChat { get; set; }
        public bool AllowItemAdds { get; set; }

        [Column("created_on")]
        public DateTime CreatedAt { get; set; }

        public DateTime EventDate { get; set; }

        public bool Deleted { get; set; }

        public List<GiftItem> GiftItems { get; set; }
        public List<SharedLists> SharedLists { get; set; }
        public List<Favorites> Favorites { get; set; }
        public List<ListMessages> ListMessages { get; set; }
    }
}
