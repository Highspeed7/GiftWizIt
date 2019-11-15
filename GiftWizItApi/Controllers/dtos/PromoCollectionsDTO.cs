using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Controllers.dtos
{
    public class PromoCollectionsDTO
    {
        public string Name { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

        PromoCollectionsDTO()
        {
            Start_Date = DateTime.Now;
            End_Date = DateTime.Now.AddYears(50);
        }
    }
}
