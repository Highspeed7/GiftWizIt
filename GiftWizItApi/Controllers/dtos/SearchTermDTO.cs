using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class SearchTermDTO
    {
        public string SearchTerm { get; set; }
        public string UserEmail { get; set; }
        public PageRes Pager { get; set; }
    }
}
