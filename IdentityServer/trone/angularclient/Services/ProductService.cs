using angularclient.Controllers;
using angularclient.DbAccess;
using angularclient.Middlewares;
using angularclient.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Services
{
    public interface IProductService 
    {
        Task<string> Save(ProductPostSave productData, IFormFile productImage);
        Task<List<Category>> GetCategories();
        Task<List<Product>> GetAll(ProductSearchParams productSearchParams);
    }

    public class ProductService : IProductService
    {
        private IWebHostEnvironment _webHostEnvironment;
        private ProductRepository _productRepository;

        public ProductService(IWebHostEnvironment webHostEnvironment, ProductRepository productRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _productRepository = productRepository;
        }

        public async Task<string> Save(ProductPostSave productData, IFormFile productImage) 
        {
            var product = await _productRepository.Get(productData.Code);

            if (product == null)
                throw new NotFoundException();

            string newUrl = string.Empty;

            if (productImage != null && productImage.Length > 0)
            {
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

                string filePath = Directory.GetParent(_webHostEnvironment.ContentRootPath)
                    .ToString() + "\\Product_Photos\\" + newUrl.Replace("/", "\\");

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await productImage.CopyToAsync(stream);

                product.PhotoUrl = newUrl;
            }

            product.Description = productData.Description;
            product.CategoryId = productData.CategoryId;

            product.Price = productData.Price;

            await _productRepository.Update(product);

            return newUrl;
        }

        public async Task<List<Category>> GetCategories() 
        {
            return await _productRepository.GetCategories();
        }

        public async Task<List<Product>> GetAll(ProductSearchParams productSearchParams)
        {
            return await _productRepository.GetAll(productSearchParams);
        }
    }
}
