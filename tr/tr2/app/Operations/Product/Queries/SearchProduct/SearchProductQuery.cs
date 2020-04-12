using domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using AutoMapper.QueryableExtensions;
using app.Common;
using System.Linq.Dynamic.Core;

namespace app.Operations.Product.Queries.SearchProduct
{
    public class SearchProductQuery : Pager, IRequest<PagedList<SearchProductQueryResponse>>
    {
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public short? MaxUnitsInStock { get; set; }
        public short? MinUnitsInStock { get; set; }
        public bool? Discontinued { get; set; }

        public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PagedList<SearchProductQueryResponse>>
        {
            private readonly INorthwindContext _context;
            private readonly IMapper _mapper;
            public SearchProductQueryHandler(INorthwindContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PagedList<SearchProductQueryResponse>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
            {
                var mainQuery = _context.Products
                    .Where(x =>
                        (string.IsNullOrEmpty(request.ProductName) || x.ProductName.Contains(request.ProductName))
                        && (!request.SupplierId.HasValue || request.SupplierId == x.SupplierId)
                        && (!request.CategoryId.HasValue || request.CategoryId == x.CategoryId)
                        && (!request.Discontinued.HasValue || request.Discontinued == x.Discontinued)
                        && (!request.MaxUnitsInStock.HasValue || request.MaxUnitsInStock >= x.UnitsInStock)
                        && (!request.MinUnitsInStock.HasValue || request.MinUnitsInStock <= x.UnitsInStock)
                    ).ProjectTo<SearchProductQueryResponse>(_mapper.ConfigurationProvider);

                return await request.CreatePagedList(mainQuery);
            }
        }
    }
}
