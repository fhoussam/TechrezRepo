using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [AllowAnonymous]

    public class SecurityController : ControllerBase
    {
        private IAntiforgery _antiForgery;
        private IWebHostEnvironment _webHostEnvironment;
        public SecurityController(IAntiforgery antiForgery, IWebHostEnvironment webHostEnvironment)
        {
            _antiForgery = antiForgery;
            _webHostEnvironment = webHostEnvironment;
        }

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

        [Route("antiforgery")]
        [HttpGet]
        public IActionResult GenerateAntiForgeryTokens()
        {
            var tokens = _antiForgery.GetAndStoreTokens(HttpContext);
            Response.Cookies.Append("XSRF-REQUEST-TOKEN", tokens.RequestToken, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = false
            });
            return NoContent();
        }
    }
}