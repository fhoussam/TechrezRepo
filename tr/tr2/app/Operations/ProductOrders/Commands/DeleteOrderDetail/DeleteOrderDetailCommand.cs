using domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Common.Exceptions;

namespace app.Operations.ProductOrders.Commands.DeleteOrderDetail
{
    public class DeleteOrderDetailCommand : IRequest<int>
    {
        public int? ProductID { get; set; }
        public int? OrderID { get; set; }

        public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, int>
        {
            private readonly INorthwindContext _context;

            public DeleteOrderDetailCommandHandler(INorthwindContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
            {
                var toDelete = await _context.OrderDetails.SingleOrDefaultAsync
                    (x => x.ProductId == request.ProductID && x.OrderId == request.OrderID);

                if (toDelete == null)
                    throw new DomainBadRequestException();

                else 
                {
                    _context.OrderDetails.Remove(toDelete);
                    return await _context.SaveChangeAsyc();
                }
            }
        }
    }
}
