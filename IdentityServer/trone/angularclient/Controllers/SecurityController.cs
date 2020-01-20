using angularclient.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using angularclient.Filters;
using Microsoft.AspNetCore.Hosting;
using EnvironmentExtensions;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
[IgnoreAntiforgeryToken]
[AllowAnonymous]

public class SecurityController : Controller
{
    private IAntiforgery _antiForgery;
    private IWebHostEnvironment _webHostEnvironment;
    public SecurityController(IAntiforgery antiForgery, IWebHostEnvironment webHostEnvironment)
    {
        _antiForgery = antiForgery;
        _webHostEnvironment = webHostEnvironment;
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

    [Route("usercontext")]
    [HttpGet]
    [CustomAntiForgery]
    public IActionResult UserContext()
    {
        if (_webHostEnvironment.IsFrontDevMode()) 
        {
            return Ok(new UserContext()
            {
                AuthTime = "6516515616",
                Birthdate = "16/10/1988",
                Email = "houssamfertaq@gmail.com",
                FavColor = "navy",
                Gender = "male",
                Name = "Houssam FERTAQ",
                Roles = new List<string>() { "admin", "techrezuser 03" }.ToArray(),
            });
        }

        else if (User.Identity.IsAuthenticated)
        {
            var claimsIdentity = (User.Identity as ClaimsIdentity);
            var userContext = new UserContext()
            {
                AuthTime = claimsIdentity.FindFirst("auth_time").Value,
                Birthdate = claimsIdentity.FindFirst("birthdate").Value,
                Email = claimsIdentity.FindFirst("email").Value,
                FavColor = claimsIdentity.FindFirst("favcolor").Value,
                Gender = claimsIdentity.FindFirst("gender").Value,
                Name = claimsIdentity.FindFirst("name").Value,
                Roles = claimsIdentity.FindAll("role").Select(x => x.Value).ToArray()
            };

            return Ok(userContext);
        }
        else
            return Ok(new UserContext() { Roles = new string[] { "visitor" } });
    }

    [Route("challengeoidc")]
    [HttpGet]
    public IActionResult ChallengeOidc(string returnUrl)
    {
        AuthenticationProperties authenticationProperties = new AuthenticationProperties()
        {
            RedirectUri = returnUrl,
        };

        if (User.Identity.IsAuthenticated)
        {
            return Redirect("/admin/users");
        }
        else
            return new ChallengeResult("oidc", authenticationProperties);
    }

    [Route("logout")]
    [HttpGet]
    public IActionResult Logout()
    {
        return SignOut(new AuthenticationProperties()
        {
            RedirectUri = "https://localhost:44301/home",
        }, "Cookies", "oidc");
    }

    [Route("jwtlogoutcallback")]
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public IActionResult JwtLogoutCallback()
    {
        //blacklist access token
        return Ok();
    }
}