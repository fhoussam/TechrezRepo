using app.Operations.Category.Queries;
using app.Operations.Product.Commands.EditProduct;
using app.Operations.Product.Queries.GetProductDetails;
using app.Operations.Product.Queries.SearchProduct;
using app.Operations.Supplier.Queries;
using AutoMapper;
using domain.Entities;

namespace app.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, SearchProductQueryResponse>();
            CreateMap<Categories, GetAllCategoriesResponse>();
            CreateMap<Suppliers, GetAllSuppliersResponse>();
            CreateMap<Products, ProductDetails>()
                .ForMember(destination => destination.Supplier, opt => opt.MapFrom(source => source.Supplier.CompanyName))
                ;
            CreateMap<EditProductCommand, Products>();
        }
    }
}
