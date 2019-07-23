using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace angularclient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Route("nonsecure")]
        public string NonSecure()
        {
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