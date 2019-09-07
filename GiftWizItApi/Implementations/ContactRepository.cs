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

        public async Task<Contacts> GetContactByEmailGuid(string emailId)
        {
            var result = await Context.Contacts.Where(c => c.VerifyGuid == emailId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> GetContactIdByUserId(string userId)
        {
            return await Context.Contacts.Where(c => c.UserId == userId).Select(c => c.ContactId).SingleAsync();
        }
    }
}
