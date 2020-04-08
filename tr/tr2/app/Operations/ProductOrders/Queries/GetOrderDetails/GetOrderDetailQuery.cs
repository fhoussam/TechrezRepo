using domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using app.Common.Exceptions;

namespace app.Operations.ProductOrders.Queries.GetOrderDetails
{
    public class GetOrderDetailQuery : 
        IRequest<IGetOrderDetailResponse>
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public bool ForEdit { get; set; }

        public GetOrderDetailQuery(int orderID, int productID, bool forEdit)
        {
            OrderID = orderID;
            ProductID = productID;
            ForEdit = forEdit;
        }

        public class GetOrderDetailQueryHandler : 
            IRequestHandler<GetOrderDetailQuery, IGetOrderDetailResponse>
        {
            private readonly INorthwindContext _context;

            public GetOrderDetailQueryHandler(INorthwindContext context)
            {
                _context = context;
            }

            public async Task<IGetOrderDetailResponse> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
            {
                if (request.ForEdit)
                    return await HandleEditMode(request, cancellationToken);
                else
                    return await HandleDisplayMode(request, cancellationToken);
            }

            private async Task<GetOrderDetailsForEditResponse> HandleEditMode(GetOrderDetailQuery request, CancellationToken cancellationToken)
            {
                var mainTask = (from od in _context.OrderDetails
                                join o in _context.Orders on od.OrderId equals o.OrderId
                                join c in _context.Customers on o.CustomerId equals c.CustomerId
                                join e in _context.Employees on o.EmployeeId equals e.EmployeeId
                                where od.OrderId == request.OrderID && od.ProductId == request.ProductID
                                select new GetOrderDetailsForEditResponse()
                                {
                                    CustomerId = o.CustomerId,
                                    EmployeeId = o.EmployeeId,
                                    OrderDate = o.OrderDate,
                                    RequiredDate = o.RequiredDate,
                                    ShippedDate = o.ShippedDate,
                                    ShipAddress = o.ShipAddress,
                                    ShipCity = o.ShipCity,
                                    ShipRegion = o.ShipRegion,
                                    ShipPostalCode = o.ShipPostalCode,
                                    ShipCountry = o.ShipCountry,
                                    Quantity = od.Quantity,
                                }).SingleOrDefaultAsync();

                var companiesTask = _context.Customers.Select(x => new { x.CustomerId, x.CompanyName }).ToListAsync();
                var employeesTask = _context.Employees.Select(x => new { x.EmployeeId, FullName = x.FirstName + " " + x.LastName }).ToListAsync();

                await Task.WhenAll(mainTask, companiesTask, employeesTask);

                var result = mainTask.Result;

                if (mainTask == null)
                    throw new DomainBadRequestException();

                result.Companies = companiesTask.Result.ToDictionary(x => x.CustomerId, x => x.CompanyName);
                result.Empployees = employeesTask.Result.ToDictionary(x => x.EmployeeId, x => x.FullName);

                return result;
            }

            private async Task<GetOrderDetailForDisplayResponse> HandleDisplayMode(GetOrderDetailQuery request, CancellationToken cancellationToken)
            {
                var toDisplay = await (from od in _context.OrderDetails
                                       join o in _context.Orders on od.OrderId equals o.OrderId
                                       join c in _context.Customers on o.CustomerId equals c.CustomerId
                                       join e in _context.Employees on o.EmployeeId equals e.EmployeeId
                                       where od.OrderId == request.OrderID && od.ProductId == request.ProductID
                                       select new GetOrderDetailForDisplayResponse()
                                       {
                                           CompanyName = c.CompanyName,
                                           EmployeeFirstName = e.FirstName,
                                           EmployeeFirstNameLastName = e.LastName,
                                           OrderDate = o.OrderDate,
                                           RequiredDate = o.RequiredDate,
                                           Quantity = od.Quantity,
                                           ShipAddress = o.ShipAddress,
                                           ShipCity = o.ShipCity,
                                           ShipCountry = o.ShipCountry,
                                           ShippedDate = o.ShippedDate,
                                           ShipPostalCode = o.ShipPostalCode,
                                           ShipRegion = o.ShipRegion,
                                       }).SingleOrDefaultAsync();

                if (toDisplay == null)
                    throw new DomainBadRequestException();

                return toDisplay;
            }
        }
    }

    public interface IGetOrderDetailResponse {}
}
