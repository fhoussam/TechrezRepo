using Api.Dto.Get;
using Api.Dto.Post;
using Api.Dto.Put;
using Dal.Models;
using System;

namespace Api.Dto
{
    public static class XConverters
    {
        public static ProductDtoGet ToDtoGet(this Product model)
        {
            return new ProductDtoGet()
            {
                Id = model.Id,
                Description = model.Description,
                CategoryDescription = model.Category.Description,
                CategoryID = model.CategoryID,
                Price = model.Price,
                Stock = model.Stock
            };
        }

        public static Product ToModel(this ProductDtoPost dto)
        {
            return new Product()
            {
                CategoryID = dto.CategoryID,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }

        public static Product ToModel(this ProductDtoPut dto)
        {
            return new Product()
            {
                Id = dto.Id,
                CategoryID = dto.CategoryID,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }
    }
}
