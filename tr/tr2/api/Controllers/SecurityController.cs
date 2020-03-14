using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        [Route("ChallengeAngularUser")]
        [HttpGet]
        public IActionResult ChallengeAngularUser(string returnUrl)
        {
            if (User.Identity.IsAuthenticated && string.IsNullOrEmpty(returnUrl))
            {
                return Redirect("/");
            }

            AuthenticationProperties authenticationProperties = new AuthenticationProperties()
            {
                RedirectUri = returnUrl,
            };

            return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme, authenticationProperties);
        }
    }
}