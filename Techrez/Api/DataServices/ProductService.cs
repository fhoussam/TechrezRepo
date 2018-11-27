using Api.Dto.Post;
using Api.Dto.Put;
using Dal;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataServices
{
    public interface IProductService
    {
        Task<object> GetProductAsync();
        Task<object> GetProductByIdAsync(int id);
        Task<object> UpdateProductAsync(ProductDtoPut dto);
        Task DeleteProductAsync(int id);
        Task<int> AddProductAsync(ProductDtoPost dto);
        void InitData();
    }

    public class ProductService : IProductService
    {
        private readonly IDalService _dal;
        private object GetDto(Product p)
        {
            return new
            {
                p.Id,
                p.Description,
                p.Stock,
                p.Price,
                p.CategoryID
            };
        }

        public ProductService(IDalService dal)
        {
            _dal = dal;
        }

        public async Task<object> GetProductAsync()
        {
            var om = await _dal.GetAllProductsAsync();
            return om.Select(x => GetDto(x));
        }

        public async Task<object> GetProductByIdAsync(int id)
        {
            var om = await _dal.GetProductByIdAsync(id);
            return GetDto(om);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _dal.DeleteProductAsync(id);
        }

        public async Task<object> UpdateProductAsync(ProductDtoPut dto)
        {
            var im = new Product()
            {
                Id = dto.Id,
                CategoryID = dto.CategoryID,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };
            var om = await _dal.UpdateProductAsync(im);
            return GetDto(om);
        }

        public async Task<int> AddProductAsync(ProductDtoPost dto)
        {
            var im = new Product()
            {
                CategoryID = dto.CategoryID,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };
            var om = await _dal.AddProductAsync(im);
            return om;
        }

        public void InitData()
        {
            _dal.InitData();
        }
    }
}