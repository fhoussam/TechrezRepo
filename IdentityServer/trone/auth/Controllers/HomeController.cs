using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using auth.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace auth.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IEmailSender emailSender, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _emailSender = emailSender;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Route("nonsecure")]
        public string NonSecure()
        {
            return "Not secure";
        }

        [AllowAnonymous]
        public async Task<string> TestIdentity() 
        {
            var result = await _signInManager.PasswordSignInAsync("houssamfertaq@gmail.com", "H0u$$@m2018", false, false);
            return "tested";
        }

        //pure c#
        public void TestSendGrid()
        {
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("houssamfertaq@gmail.com", "Techrez team");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("houssamfertaq@gmail.com", "Techrez team");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var result = client.SendEmailAsync(msg).Result;
        }

        //using fancy DI, inorder to simulate what asp identity would do
        public void TestSendGrid2()
        {
            var email = "houssamfertaq@gmail.com";
            var subject = "Sending with SendGrid is Fun";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            _emailSender.SendEmailAsync(email, subject, htmlContent);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
