using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class PartnersRepository : Repository<Partners>, IPartnersRepository
    {
        public PartnersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Add(string name, string domain)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Partners>> GetPartnerAsync(string domain)
        {
            return await Context.Partners.Where(p => p.Domain == domain).ToListAsync();
        }
    }
}
