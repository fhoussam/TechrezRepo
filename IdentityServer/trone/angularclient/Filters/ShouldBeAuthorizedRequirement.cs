using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentExtensions;
using Microsoft.AspNetCore.Hosting;

namespace angularclient.Filters
{
    public class ShouldBeAuthorizedRequirement : IAuthorizationRequirement
    {

    }

    public class CanMakeCriticalChangesHandler : AuthorizationHandler<ShouldBeAuthorizedRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CanMakeCriticalChangesHandler(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            this.httpContextAccessor = httpContextAccessor;
            this._webHostEnvironment = webHostEnvironment;
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
            var roles = claimsIdentity.FindAll(claimsIdentity.RoleClaimType).Select(x => x.Value).ToArray();

            if (_webHostEnvironment.IsFrontDevMode() || roles.Contains("admin") || roles.Contains("techrezuser 03"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
