using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class GListShareDTO
    {
        public int G_List_Id { get; set; }
        public string Password { get; set; }
        public bool IsPublic { get; set; }
        public List<ContactDTO> Contacts { get; set; }
    }
}
