using app.Operations.Product.Queries.GetProductDetails;
using app.Operations.Product.Queries.SearchProduct;
using AutoMapper;
using domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, SearchProductQueryResponse>();
            CreateMap<Products, ProductDetails>();
        }
    }
}
