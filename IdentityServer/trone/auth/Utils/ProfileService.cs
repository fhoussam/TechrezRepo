using auth.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace auth.Utils
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> mUserManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            mUserManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            ApplicationUser user = await mUserManager.GetUserAsync(context.Subject);

            //main profile
            context.IssuedClaims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            //context.IssuedClaims.Add(new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber));

            //roles
            IList<string> roles = await mUserManager.GetRolesAsync(user);
            IList<Claim> roleClaims = new List<Claim>();
            foreach (string role in roles)
            {
                roleClaims.Add(new Claim(JwtClaimTypes.Role, role));
            }
            context.IssuedClaims.AddRange(roleClaims);

            //claims
            var claims = await mUserManager.GetClaimsAsync(user);
            context.IssuedClaims.AddRange(claims);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
