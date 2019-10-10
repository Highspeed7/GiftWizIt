using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Models
{
    public class Page
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }

        public Page()
        {
            PageCount = 1;
            PageSize = 20;
        }
    }
}
