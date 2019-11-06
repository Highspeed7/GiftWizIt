using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class GiftListDto
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string Password { get; set; }
        public bool RestrictChat { get; set; }
        public bool AllowItemAdds { get; set; }

        public List<GiftItemDTO> GiftItems { get; set; }
    }
}
