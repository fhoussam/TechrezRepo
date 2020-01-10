using angularclient.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
[IgnoreAntiforgeryToken]
[AllowAnonymous]

public class SecurityController : Controller
{
    private IAntiforgery _antiForgery;
    public SecurityController(IAntiforgery antiForgery)
    {
        _antiForgery = antiForgery;
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
    [ValidateAntiForgeryToken]
    public IActionResult UserContext()
    {
        if (User.Identity.IsAuthenticated)
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
}