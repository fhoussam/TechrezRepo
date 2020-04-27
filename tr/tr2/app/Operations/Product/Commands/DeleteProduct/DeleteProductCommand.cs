using domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using app.Common.Exceptions;

namespace app.Operations.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<int>
    {
        public readonly int[] Ids;
        public DeleteProductCommand(int[] ids)
        {
            Ids = ids;
        }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
        {
            private readonly INorthwindContext _context;

            public DeleteProductCommandHandler(INorthwindContext context)
            {
                _context = context;
            }
            
            public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                if (request.Ids.Length == 0)
                {
                    throw new DomainBadRequestException();
                }
                else 
                {
                    var toRemove = await _context.Products.Where(x => request.Ids.Contains(x.ProductId)).ToListAsync();
                    if (toRemove.Count != request.Ids.Length)
                        throw new DomainBadRequestException();
                    else
                    {
                        _context.Products.RemoveRange(toRemove);
                        //return await _context.SaveChangeAsyc();
                        return 0;
                    }
                }
            }
        }
    }
}
