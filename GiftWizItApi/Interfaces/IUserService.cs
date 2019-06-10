using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiftWizItApi.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserIdAsync(ClaimsPrincipal user);
        Task<UserFacebook> SetUserFacebookAssoc(string userId, string fbId);
    }
}
