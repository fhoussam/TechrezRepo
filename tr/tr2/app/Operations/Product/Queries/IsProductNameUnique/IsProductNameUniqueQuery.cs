using domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace app.Operations.Product.Queries.IsProductNameUnique
{
    public class IsProductNameUniqueQuery : IRequest<bool>
    {
        public string ProductName { get; set; }

        public class IsProductNameUniqueQueryHandler : IRequestHandler<IsProductNameUniqueQuery, bool>
        {
            INorthwindContext _context;

            public IsProductNameUniqueQueryHandler(INorthwindContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(IsProductNameUniqueQuery request, CancellationToken cancellationToken)
            {
                return await _context.Products.AnyAsync(x=>x.ProductName.ToLower() == request.ProductName.ToLower());
            }
        }
    }
}
