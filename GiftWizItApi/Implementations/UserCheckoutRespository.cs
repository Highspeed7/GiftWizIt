using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace GiftWizItApi.Implementations
{
    public class UserCheckoutRespository : Repository<UserCheckout>, IUserCheckoutRepository
    {
        public UserCheckoutRespository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserCheckout>> GetUserCheckout(string userId)
        {
            var result = await Context.Users
                .Where(u => u.UserId == userId)
                .IncludeFilter(u => u.UserCheckouts
                    .Where(uc => uc.Completed == false))
                .FirstOrDefaultAsync();

            return result.UserCheckouts;
        }
    }
}
