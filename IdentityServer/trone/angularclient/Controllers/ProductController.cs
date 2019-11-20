using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace angularclient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //worked !
        [Authorize]
        [Route("Logout")]
        public async Task Logout()
        {
            try
            {
                int cookiesRequest_count_avant = HttpContext.Request.Cookies.Count();
                await HttpContext.SignOutAsync("Dynamic");
                await HttpContext.SignOutAsync("oidc");
                await HttpContext.SignOutAsync("Cookies");
                HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
                int cookiesRequest_count_after = HttpContext.Request.Cookies.Count();
            }
            catch (Exception ex)
            {

            }
        }

        //did not work
        [Authorize]
        [Route("logoutapilevel")]
        [HttpPost]
        public async Task LogoutApiLevel()
        {
            try
            {
                int cookiesRequest_count_avant = HttpContext.Request.Cookies.Count();
                await HttpContext.SignOutAsync("Dynamic");
                await HttpContext.SignOutAsync("oidc");
                await HttpContext.SignOutAsync("Cookies");
                HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
                int cookiesRequest_count_after = HttpContext.Request.Cookies.Count();
            }
            catch (Exception eeee)
            {
            }
        }

        [Route("nonsecure")]
        public string NonSecure()
        {
            //testing form data for token endpoint
            //var first_name = HttpContext.Request.Form["first_name"];
            //var last_name = HttpContext.Request.Form["last_name"];
            return "Not secure";
        }

        [Authorize]
        [Route("secure")]
        public string Secure()
        {
            var user_claims = User.Claims;
            var user_identity = User.Identity;
            var accessToken = HttpContext.GetTokenAsync("access_token").Result;
            var refreshToken = HttpContext.GetTokenAsync("refresh_token").Result;
            return "Secure";
        }
    }
}