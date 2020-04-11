using domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Common.Exceptions;

namespace app.Operations.ProductOrders.Commands.EditOrderDetail
{
    public class EditOrderDetailCommand : IRequest<int>
    {
		public int? OrderId { get; set; }
		public int? ProductId { get; set; }
		public string CustomerId { get; set; }
		public int? EmployeeId { get; set; }
		public short? Quantity { get; set; }
		public DateTime? OrderDate { get; set; }
		public DateTime? RequiredDate { get; set; }
		public DateTime? ShippedDate { get; set; }
		public string ShipAddress { get; set; }
		public string ShipPostalCode { get; set; }

		public class EditOrderDetailCommandHandler : IRequestHandler<EditOrderDetailCommand, int>
		{
			private readonly INorthwindContext _context;

			public EditOrderDetailCommandHandler(INorthwindContext context)
			{
				_context = context;
			}

			public async Task<int> Handle(EditOrderDetailCommand request, CancellationToken cancellationToken)
			{
				var toEdit = await _context.OrderDetails.Include(x=>x.Order).SingleOrDefaultAsync
					(x => x.ProductId == request.ProductId && x.OrderId == request.OrderId);

				//better than doing it  at validator level, which would require multiple db requests
				if (toEdit == null)
					throw new DomainBadRequestException();

				else
				{
					toEdit.Order.CustomerId = request.CustomerId;
					toEdit.Order.EmployeeId = request.EmployeeId.Value;
					toEdit.Order.OrderDate = request.OrderDate.Value;
					toEdit.Order.RequiredDate = request.RequiredDate.Value;
					toEdit.Order.ShippedDate = request.ShippedDate.Value;
					toEdit.Order.ShipAddress = request.ShipAddress;
					toEdit.Order.ShipPostalCode = request.ShipPostalCode;
					toEdit.Quantity = request.Quantity.Value;

					return await _context.SaveChangeAsyc();
				}
			}
		}
	}
}
