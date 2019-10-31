using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class PromoItemsRepository: Repository<PromoItems>, IPromoItemsRepository
    {
        public PromoItemsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
