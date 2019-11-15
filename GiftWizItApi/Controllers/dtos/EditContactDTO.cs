using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class EditContactDTO
    {
        public int GiftListId { get; set; }
        public string NewName { get; set; }
        public string NewPass { get; set; }
        public bool IsPublic { get; set; }
        public bool RestrictChat { get; set; }
        public bool AllowAdds { get; set; }
    }
}
