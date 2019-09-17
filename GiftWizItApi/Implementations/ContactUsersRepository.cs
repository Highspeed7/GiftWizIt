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

        public async Task<IEnumerable<ContactUsers>> GetAllUserContacts(string userId, bool includeDeleted = false)
        {
            List<ContactUsers> results;
            if(includeDeleted == true)
            {
                results = await Context.ContactUsers.Include(cu => cu.Contact).Where(cu => cu.UserId == userId).ToListAsync();
            }
            else
            {
                results = await Context.ContactUsers.Include(cu => cu.Contact).Where(cu => cu.UserId == userId && cu.Deleted != true).ToListAsync();
            }
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
                    ContactAlias = existingContact.Alias
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
                    UserId = userId,
                    ContactAlias = contact.Name
                };
            }

            base.Add(newContact);
            return newContact;
        }
    }
}
