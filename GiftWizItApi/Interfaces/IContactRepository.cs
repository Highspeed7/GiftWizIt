using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IContactRepository: IRepository<Contacts>
    {
        Task Add(AddContactDTO contact, string userId);
        Task<Contacts> GetContactByEmail(string email);
        Task<Contacts> GetContactByEmailGuid(string emailId);
    }
}
