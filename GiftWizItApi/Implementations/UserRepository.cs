using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context): base(context)
        {
        }

        public async Task<Users> GetUserByIdAsync(string id)
        {
            return await Context.Users
                .Where(u => u.UserId == id)
                .SingleOrDefaultAsync();
        }

        public Users Add(string userId)
        {
            var user = new Users
            {
                UserId = userId
            };

            return Context.Users.Add(user).Entity;
        }
    }
}
