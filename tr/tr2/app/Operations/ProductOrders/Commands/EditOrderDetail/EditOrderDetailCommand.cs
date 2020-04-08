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
		public int? ProductID { get; set; }
		public string CustomerID { get; set; }
		public int? EmployeeID { get; set; }
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
					(x => x.ProductId == request.ProductID && x.OrderId == request.OrderId);

				if (toEdit == null)
					throw new DomainBadRequestException();

				else
				{
					toEdit.Order.CustomerId = request.CustomerID;
					toEdit.Order.EmployeeId = request.EmployeeID.Value;
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
