using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace app.Operations.Product.Queries.GetProductDetails
{
    public class GetProductDetailsQuery : IRequest<ProductDetails>
    {
        public int ProductId { get; set; }

        public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetails>
        {
            private readonly INorthwindContext _context;
            private readonly IMapper _mapper;

            public GetProductDetailsQueryHandler(INorthwindContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProductDetails> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
            {
                var data = await _context.Products.SingleOrDefaultAsync(x => x.ProductId == request.ProductId);
                return _mapper.Map<ProductDetails>(data);
            }
        }
    }
}
