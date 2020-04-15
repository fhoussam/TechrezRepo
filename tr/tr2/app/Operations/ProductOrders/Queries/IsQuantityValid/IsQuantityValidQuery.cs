using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace app.Operations.ProductOrders.Queries.IsQuantityValidQuery
{
    public class IsQuantityValidQuery : IRequest<bool>
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public short? Quantity { get; set; }

        public class IsQuantityValidQueryHandler : IRequestHandler<IsQuantityValidQuery, bool>
        {
            private readonly INorthwindContext _context;

            public IsQuantityValidQueryHandler(INorthwindContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(IsQuantityValidQuery request, CancellationToken cancellationToken)
            {
                var queryResult = await (
                    from od in _context.OrderDetails
                    join p in _context.Products
                    on od.ProductId equals p.ProductId
                    where od.OrderId == request.OrderId && p.ProductId == request.ProductId
                    select new
                    {
                        OldQuanity = od.Quantity,
                        UnitsInStock = p.UnitsInStock,
                    }).ToListAsync();

                //as we dont have any creation mode for this use case
                if (queryResult.Count != 1)
                    throw new ValidationException();
                else {
                    var sumQuantityPerProduct = await _context.OrderDetails.Where(x => x.ProductId == request.ProductId).SumAsync(x => x.Quantity);
                    return request.Quantity <= queryResult[0].OldQuanity
                        || queryResult[0].UnitsInStock - sumQuantityPerProduct - request.Quantity + queryResult[0].OldQuanity > 0;
                }
            }
        }
    }
}
