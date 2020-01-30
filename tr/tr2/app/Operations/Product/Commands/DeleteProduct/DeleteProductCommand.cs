using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace app.Operations.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int ProductId { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
        {
            private readonly INorthwindContext _context;

            public DeleteProductCommandHandler(INorthwindContext context)
            {
                _context = context;
            }
            
            public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var toRemove = await _context.Products.SingleOrDefaultAsync(x => x.ProductId == request.ProductId);
                _context.Products.Remove(toRemove);
                return await _context.SaveChangeAsyc();
            }
        }
    }
}
