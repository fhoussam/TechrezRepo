using angularclient.DbAccess;
using angularclient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace angularclient.Controllers
{
    public class TechRezException : Exception
    {
        public TechRezException(string message) : base(message) { }
    }

    public class PeoductPostSave
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : TechRezBaseRepoController<Product, ProductRepository>
    {
        private ProductRepository _productRepository;
        private HostingEnvironment _hostingEnvironment;

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(
            [ModelBinder(BinderType = typeof(JsonModelBinder))] PeoductPostSave productData, 
                IFormFile productImage)
        {
            var product = await _productRepository.Get(productData.Code);
            string newUrl = string.Empty;

            if (productImage != null && productImage.Length > 0) {
                //var filePath = Path.GetTempFileName();
                var format = "yyyyMMddHHmmssffff";
                var timestamp = DateTime.Now.ToString(format);
                var dateExists = DateTime.TryParseExact
                (
                    s: product.PhotoUrl.Split('_').Last().Split('.')[0],
                    format: format,
                    provider: null,
                    style: 0,
                    out var parsedDate
                );

                newUrl = !dateExists
                    ? product.PhotoUrl.Replace(".jpg", "_" + DateTime.Now.ToString(format) + ".jpg")
                    : product.PhotoUrl.Replace(parsedDate.ToString(format), timestamp);

                string filePath = Directory.GetParent(_hostingEnvironment.ContentRootPath)
                    .ToString() + "\\Product_Photos\\" + newUrl.Replace("/", "\\");

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await productImage.CopyToAsync(stream);

                product.PhotoUrl = newUrl;
            }

            product.Description = productData.Description;
            product.CategoryId = productData.CategoryId;
            
            product.Price = productData.Price;

            await _productRepository.Update(product);

            return Ok(new { path = newUrl });
        }

        [HttpGet]
        [Route("images/{category}/{imageName}")]
        public IActionResult getimage(string category, string imageName)
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
        public async Task<IActionResult> Categories()
        {
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