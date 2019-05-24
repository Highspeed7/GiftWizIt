using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class ContactRepository : Repository<Contacts>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task Add(AddContactDTO contact, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Contacts> GetContactByEmail(string email)
        {
            var result = await Context.Contacts.Where(c => c.Email == email).FirstOrDefaultAsync();
            return result;
        }
    }
}
