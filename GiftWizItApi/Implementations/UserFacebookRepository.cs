using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class UserFacebookRepository : Repository<UserFacebook>, IUserFacebookRepository
    {
        public UserFacebookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public UserFacebook Add(string userId, string fbId)
        {
            return Context.UserFacebook.Add(new UserFacebook()
            {
                UserId = userId,
                FacebookId = fbId
            }).Entity;
        }

        public async Task<UserFacebook> GetUserIdByFbId(string fbId)
        {
            return await Context.UserFacebook.Where(uf => uf.FacebookId == fbId).FirstOrDefaultAsync();
        }
    }
}
