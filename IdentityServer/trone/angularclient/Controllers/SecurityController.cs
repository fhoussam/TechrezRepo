using angularclient.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]

public class SecurityController : Controller
{
    private IAntiforgery _antiForgery;
    public SecurityController(IAntiforgery antiForgery)
    {
        _antiForgery = antiForgery;
    }

    [Route("antiforgery")]
    [HttpGet]
    [IgnoreAntiforgeryToken]
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
    public IActionResult UserContext() 
    {
        if (User.Identity.IsAuthenticated)
        {
            var claimsIdentity = (User.Identity as ClaimsIdentity);
            var userContext = new UserContext()
            {
                Birthdate = claimsIdentity.FindFirst("birthDate").Value,
                Email = claimsIdentity.FindFirst("email").Value,
                FavColor = claimsIdentity.FindFirst("favColor").Value,
                Gender = claimsIdentity.FindFirst("gender").Value,
                Name = claimsIdentity.FindFirst("name").Value,
            };

            return Ok(userContext);
        }
        else
            return Ok();
    }

    [Route("challengeoidc")]
    [HttpGet]
    public IActionResult ChallengeOidc(string returnUrl)
    {
        AuthenticationProperties authenticationProperties = new AuthenticationProperties()
        {
            RedirectUri = returnUrl,
        };

        if (User.Identity.IsAuthenticated) {
            return Redirect("/admin/users");
        }
        else
            return new ChallengeResult("oidc", authenticationProperties);
    }

    [Authorize]
    [Route("logout")]
    [HttpGet]
    public IActionResult Logout() 
    {
        //HttpContext.SignInAsync()
        
        return SignOut(new AuthenticationProperties()
        {
            RedirectUri = "https://localhost:44301/home",
        }, "Cookies", "oidc");

        //return new SignOutResult("oidc", new AuthenticationProperties()
        //{
        //    RedirectUri = "home",
        //});
    }
}