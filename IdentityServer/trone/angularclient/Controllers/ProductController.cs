using angularclient.DbAccess;
using angularclient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        public ProductController(ProductRepository repository) : base(repository)
        {
            this._productRepository = repository;
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