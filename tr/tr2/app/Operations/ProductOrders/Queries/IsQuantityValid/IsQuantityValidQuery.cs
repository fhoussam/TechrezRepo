using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace app.Operations.ProductOrders.Queries.IsQuantityValidQuery
{
    public class IsQuantityValidQuery : IRequest<bool>
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public class IsQuantityValidQueryHandler : IRequestHandler<IsQuantityValidQuery, bool>
        {
            private readonly INorthwindContext _context;

            public IsQuantityValidQueryHandler(INorthwindContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(IsQuantityValidQuery request, CancellationToken cancellationToken)
            {
                var query = await(
                    from od in _context.OrderDetails
                    join p in _context.Products
                    on od.ProductId equals p.ProductId
                    where od.OrderId == request.OrderID && p.ProductId == request.ProductID
                    select new
                    {
                        OldQuanity = od.Quantity,
                        UnitsInStock = p.UnitsInStock,
                    }).ToListAsync();

                if (query.Count != 1)
                    throw new Exception();

                return request.Quantity < query[0].OldQuanity || request.Quantity - query[0].OldQuanity <= query[0].UnitsInStock;
            }
        }
    }
}
