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
    }
}
