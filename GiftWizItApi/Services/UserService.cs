using GiftWizItApi.Constants;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiftWizItApi.Services
{
    public class UserService: IUserService
    {
        private ClaimsPrincipal User = new ClaimsPrincipal();
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor context;

        public UserService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public async Task<string> GetUserIdAsync()
        {
            var authedUser = context.HttpContext.User;

            var userId = authedUser.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var email = authedUser.Claims.First(e => e.Type == "emails").Value;

            var identityProvider = authedUser.HasClaim(e => e.Type == "http://schemas.microsoft.com/identity/claims/identityprovider") ?
                authedUser.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/identityprovider").Value : null;

            if(identityProvider == null)
            {
                return userId;
            }

            UserFacebook userFbAssoc = null;

            if(identityProvider == AppConstants.FacebookIdentityProvider)
            {
                userFbAssoc = await unitOfWork.UserFacebook.GetUserIdByFbId(userId);

                if (userFbAssoc != null)
                {
                    return userFbAssoc.UserId;
                }else
                {
                    // Get the user from users table
                    var user = await unitOfWork.Users.GetUserByEmailAsync(email);

                    var fbAssoc = await SetUserFacebookAssoc(user.UserId, userId);
                    return fbAssoc.UserId;
                }
            }
            return userId;
        }

        public async Task<UserFacebook> SetUserFacebookAssoc(string userId, string fbId)
        {
            UserFacebook fbAssoc = new UserFacebook()
            {
                UserId = userId,
                FacebookId = fbId
            };

            UserFacebook insertedFbAssoc = unitOfWork.UserFacebook.Add(userId, fbId);
            await unitOfWork.CompleteAsync();

            return insertedFbAssoc;
        }
    }
}
