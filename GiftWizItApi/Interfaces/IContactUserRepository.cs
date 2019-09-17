using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IContactUsersRepository: IRepository<ContactUsers>
    {
        ContactUsers Add(AddContactDTO contact, string userId, ContactDTO existingContact = null);
        Task<ContactUsers> GetUserContactById(int contactId, string userId);
        Task<IEnumerable<ContactUsers>> GetAllUserContacts(string userId, bool includeDeleted = false);
    }
}
