using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class ContactUsersRepository : Repository<ContactUsers>, IContactUsersRepository
    {
        public ContactUsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ContactUsers>> GetAllUserContacts(string userId)
        {
            var results = await Context.ContactUsers.Include(cu => cu.Contact).Where(cu => cu.UserId == userId).ToListAsync();
            return results;
        }

        public async Task<ContactUsers> GetUserContactById(int contactId, string userId)
        {
            var results = await Context.ContactUsers.Where(cu => cu.ContactId == contactId && cu.UserId == userId).FirstOrDefaultAsync();
            return results;
        }

        public ContactUsers Add(AddContactDTO contact, string userId, ContactDTO existingContact = null)
        {
            ContactUsers newContact;

            if (existingContact != null)
            {
                newContact = new ContactUsers()
                {
                    ContactId = existingContact.ContactId,
                    UserId = userId,
                };
            }
            else
            {
                newContact = new ContactUsers()
                {
                    Contact = new Contacts()
                    {
                        Email = contact.Email,
                        Name = contact.Name,
                        EmailSent = false,
                        Verified = false,
                        VerifyGuid = Guid.NewGuid().ToString()
                    },
                    UserId = userId
                };
            }

            base.Add(newContact);
            return newContact;
        }
    }
}
