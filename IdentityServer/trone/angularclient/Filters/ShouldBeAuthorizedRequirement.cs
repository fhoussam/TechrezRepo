using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Filters
{
    public class ShouldBeAuthorizedRequirement : IAuthorizationRequirement
    {

    }

    public class CanMakeCriticalChangesHandler : AuthorizationHandler<ShouldBeAuthorizedRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CanMakeCriticalChangesHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeAuthorizedRequirement requirement)
        {
            //get controller
            //get action
            //get http verb
            //get is ajax
            //list of claims
            //base.auth()

            var httpContext = httpContextAccessor.HttpContext;
            var claimsIdentity = (httpContext.User.Identity as System.Security.Claims.ClaimsIdentity);
            var roles = claimsIdentity.FindAll("role").Select(x => x.Value).ToArray();
            var isAdmin = roles.Contains("admin");

            if (isAdmin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
