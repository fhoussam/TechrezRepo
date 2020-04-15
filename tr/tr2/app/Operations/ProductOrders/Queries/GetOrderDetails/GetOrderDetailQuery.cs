using domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using app.Common.Exceptions;
using System.Collections.Generic;
using app.Common.Enums;
using static app.Operations.Config.Commands.GetDropDownListsQuery;

namespace app.Operations.ProductOrders.Queries.GetOrderDetails
{
    public class GetOrderDetailQuery : IRequest<IGetOrderDetailResponse>
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public bool? ForEdit { get; set; }

        private static readonly HashSet<DropDownListIdentifier> _requiredDropDownLists = new HashSet<DropDownListIdentifier>() 
        {
            DropDownListIdentifier.Customers,
            DropDownListIdentifier.Employees,
            DropDownListIdentifier.Countries,
        };

        public GetOrderDetailQuery(int productId, int orderId, bool forEdit)
        {
            ProductId = productId;
            OrderId = orderId;
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
                if (request.ForEdit == true)
                    return await HandleEditMode(request, cancellationToken);
                else
                    return await HandleDisplayMode(request, cancellationToken);
            }

            private async Task<GetOrderDetailsForEditResponse> HandleEditMode(GetOrderDetailQuery request, CancellationToken cancellationToken)
            {
                var mainQueryTask = (from od in _context.OrderDetails
                                join o in _context.Orders on od.OrderId equals o.OrderId
                                join c in _context.Customers on o.CustomerId equals c.CustomerId
                                join e in _context.Employees on o.EmployeeId equals e.EmployeeId
                                where od.OrderId == request.OrderId && od.ProductId == request.ProductId
                                select new GetOrderDetailsForEditResponse()
                                {
                                    OrderId = o.OrderId,
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

                await mainQueryTask;

                if (mainQueryTask.Result == null)
                    throw new DomainBadRequestException();
                else 
                {
                    var dropDownListDataTask = new GetDropDownListDataQueryhandler(_context)
                        .Handle(new Config.Commands.GetDropDownListsQuery(_requiredDropDownLists), cancellationToken);

                    mainQueryTask.Result.DropDownListData = dropDownListDataTask.Result;
                    return mainQueryTask.Result;
                }
            }

            private async Task<GetOrderDetailsForDisplayResponse> HandleDisplayMode(GetOrderDetailQuery request, CancellationToken cancellationToken)
            {
                var toDisplay = await (from od in _context.OrderDetails
                                       join o in _context.Orders on od.OrderId equals o.OrderId
                                       join c in _context.Customers on o.CustomerId equals c.CustomerId
                                       join e in _context.Employees on o.EmployeeId equals e.EmployeeId
                                       where od.OrderId == request.OrderId && od.ProductId == request.ProductId
                                       select new GetOrderDetailsForDisplayResponse()
                                       {
                                           OrderId = o.OrderId,
                                           CompanyName = c.CompanyName,
                                           EmployeeFirstName = e.FirstName,
                                           EmployeeLastName = e.LastName,
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
