using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace app.Operations.Product.Queries.SearchProduct
{
    public class SearchProductQuery : IRequest<List<SearchProductQueryResponse>>
    {
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public short? MaxUnitsInStock { get; set; }
        public short? MinUnitsInStock { get; set; }
        public bool? Discontinued { get; set; }

        public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, List<SearchProductQueryResponse>>
        {
            private readonly INorthwindContext _context;
            private readonly IMapper _mapper;
            public SearchProductQueryHandler(INorthwindContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<SearchProductQueryResponse>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
            {
                return await _context.Products
                    .Where(x =>
                        (string.IsNullOrEmpty(request.ProductName) || x.ProductName.Contains(request.ProductName))
                        && (!request.SupplierId.HasValue || request.SupplierId == x.SupplierId)
                        && (!request.CategoryId.HasValue || request.CategoryId == x.CategoryId)
                        && (!request.Discontinued.HasValue || request.Discontinued == x.Discontinued)
                        && (!request.MaxUnitsInStock.HasValue || request.MaxUnitsInStock >= x.UnitsInStock)
                        && (!request.MinUnitsInStock.HasValue || request.MinUnitsInStock <= x.UnitsInStock)
                    )
                    .ProjectTo<SearchProductQueryResponse>(_mapper.ConfigurationProvider).ToListAsync();
            }
        }
    }
}
