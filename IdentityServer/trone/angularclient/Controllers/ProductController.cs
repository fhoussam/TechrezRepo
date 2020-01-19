using angularclient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using angularclient.Services;

namespace angularclient.Controllers
{
    public class TechRezException : Exception
    {
        public TechRezException(string message) : base(message) { }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ShouldBeAuthorized")]

    public class ProductController : ControllerBase
    //: TechRezBaseRepoController<Product, ProductRepository>
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IProductService _productService;

        public ProductController(IWebHostEnvironment webHostEnvironment, IProductService productService)
        //: base(repository, webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _productService = productService;
        }

        //an endpoint for testing validation
        [HttpGet]
        [Route("validate")]
        public IActionResult Validate([FromQuery] ProductPostSave productPostSave)
        {
            return Ok("validated !");
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductSearchParams productSearchParams)
        {
            return Ok(await _productService.GetAll(productSearchParams));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.Get(id.ToString()));
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(
            [ModelBinder(BinderType = typeof(JsonModelBinder))] ProductPostSave productData, 
                IFormFile productImage)
        {
            string newImageUrl = await _productService.Save(productData, productImage);
            return Ok(new { path = newImageUrl });
        }

        [HttpGet]
        [Route("images/{category}/{imageName}")]
        [IgnoreAntiforgeryToken]
        public IActionResult getimage(string category, string imageName)
        {
            try
            {
                string directory = System.IO.Directory.GetParent(_webHostEnvironment.ContentRootPath).ToString() + "\\Product_Photos\\";
                var image = System.IO.File.OpenRead(directory + category + "\\" + imageName);
                return File(image, "image/jpeg"); //uising a FileStreamResult
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("categories")]
        [AllowAnonymous]
        public async Task<IActionResult> Categories()
        {
            return Ok(await _productService.GetCategories());
        }

        [Route("nonsecure")]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public string NonSecure()
        {
            //testing form data for token endpoint
            //var first_name = HttpContext.Request.Form["first_name"];
            //var last_name = HttpContext.Request.Form["last_name"];
            return "Not secure";
        }

        [Authorize]
        [Route("secure")]
        [IgnoreAntiforgeryToken]
        public string Secure()
        {
            var user_claims = User.Claims;
            var user_identity = User.Identity;
            var accessToken = HttpContext.GetTokenAsync("access_token").Result;
            var refreshToken = HttpContext.GetTokenAsync("refresh_token").Result;
            return "Secure";
        }
    }

    public class JsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Check the value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                // Attempt to convert the input value
                var valueAsString = valueProviderResult.FirstValue;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject(valueAsString, bindingContext.ModelType);
                if (result != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                    return Task.CompletedTask;
                }
            }

            return Task.CompletedTask;
        }
    }
}