using angularclient.DbAccess;
using angularclient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace angularclient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : TechRezBaseRepoController<Product, ProductRepository>
    {
        private ProductRepository _productRepository;
        private HostingEnvironment _hostingEnvironment;

        [HttpGet]
        [Route("images/{category}/{imageName}")]
        public IActionResult GetRandomImage(string category, string imageName) 
        {
            string directory = System.IO.Directory.GetParent(_hostingEnvironment.ContentRootPath).ToString() + "\\Product_Photos\\";
            var image = System.IO.File.OpenRead(directory + category + "\\" + imageName);
            return File(image, "image/jpeg"); //uising a FileStreamResult
        }

        public ProductController(ProductRepository repository, HostingEnvironment hostingEnvironment) : base(repository, hostingEnvironment)
        {
            this._productRepository = repository;
            this._hostingEnvironment = hostingEnvironment;
        }

        [Route("categories")]
        public async Task<IActionResult> Categories() {
            var r = await this._productRepository.GetCategories();
            return Ok(r);
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