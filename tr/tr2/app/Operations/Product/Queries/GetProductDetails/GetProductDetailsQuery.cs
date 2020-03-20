using domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using app.Common.Exceptions;

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
                var data = await _context.Products.Include(x=>x.Supplier).SingleOrDefaultAsync(x => x.ProductId == request.ProductId);

                if (data == null)
                    throw new NotFoundException(request.ProductId);

                return _mapper.Map<ProductDetails>(data);
            }
        }
    }
}
