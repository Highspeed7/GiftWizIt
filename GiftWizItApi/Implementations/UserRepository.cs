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
            var result = await Context.Users
                .Where(u => u.UserId == id)
                .SingleOrDefaultAsync();

            return result;
        }

        public Users Add(string userId, string email, string facebook_id = null)
        {
            var user = new Users
            {
                UserId = userId,
                Email = email
            };

            return Context.Users.Add(user).Entity;
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await Context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}
