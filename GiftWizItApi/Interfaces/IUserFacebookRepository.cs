using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IUserFacebookRepository: IRepository<UserFacebook>
    {
        UserFacebook Add(string userId, string fbId);
        Task<UserFacebook> GetUserIdByFbId(string fbId);
    }
}
