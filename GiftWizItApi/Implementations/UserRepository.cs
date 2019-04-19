using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
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
    }
}
