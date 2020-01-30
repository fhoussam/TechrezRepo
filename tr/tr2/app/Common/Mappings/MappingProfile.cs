using app.Operations.Product.Queries.SearchProduct;
using AutoMapper;
using domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, SearchProductQueryResponse>();
        }
    }
}
