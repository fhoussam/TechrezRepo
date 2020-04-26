using app.Operations.Product.Commands.EditProduct;
using app.Operations.Product.Queries.GetProductDetails;
using app.Operations.Product.Queries.SearchProduct;
using AutoMapper;
using domain.Entities;

namespace app.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, SearchProductQueryResponse>();
            CreateMap<Products, ProductDetails>()
                .ForMember(destination => destination.Supplier, opt => opt.MapFrom(source => source.Supplier.CompanyName))
                ;
            CreateMap<EditProductCommand, Products>();
        }
    }
}
