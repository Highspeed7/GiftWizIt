using GiftWizItApi.Implementations;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users> GetUserByIdAsync(string id);
        Users Add(string userId, string email, string name, string facebook_id = null);
        Task<Users> GetUserByEmailAsync(string email);
    }
}
