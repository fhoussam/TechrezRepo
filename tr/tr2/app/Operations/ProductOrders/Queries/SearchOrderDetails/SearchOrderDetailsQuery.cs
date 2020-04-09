using domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using app.Common;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace app.Operations.ProductOrders.Queries.SearchOrderDetails
{
    public class SearchOrderDetailsQuery : Pager, IRequest<PagedList<SearchOrderDetailsResponse>>
    {
        public int ProductId { get; set; }
        public DateTime OrderDateFrom { get; set; }
        public DateTime OrderDateTo { get; set; }

        public class SearchOrderDetailsQueryHandler : IRequestHandler<SearchOrderDetailsQuery, PagedList<SearchOrderDetailsResponse>>
        {
            private readonly INorthwindContext _context;

            public SearchOrderDetailsQueryHandler(INorthwindContext context)
            {
                _context = context;
            }

            public async Task<PagedList<SearchOrderDetailsResponse>> Handle(SearchOrderDetailsQuery request, CancellationToken cancellationToken)
            {
                var mainQuery = (
                    from od in _context.OrderDetails
                    join o in _context.Orders on od.OrderId equals o.OrderId
                    join c in _context.Customers on o.CustomerId equals c.CustomerId
                    join e in _context.Employees on o.EmployeeId equals e.EmployeeId
                    where true
                        && od.ProductId == request.ProductId
                        && o.OrderDate >= request.OrderDateFrom
                        && o.OrderDate <= request.OrderDateTo
                        && o.OrderDate.HasValue //temporary
                    select new SearchOrderDetailsResponse()
                    {
                        OrderId = o.OrderId,
                        CompanyName = c.CompanyName,
                        EmployeeFirstName = e.FirstName,
                        EmployeeLastName = e.LastName,
                        OrderDate = o.OrderDate,
                        ShipCountry = o.ShipCountry,
                    }
                    );

                return await request.CreatePagedList(mainQuery, _context.OrderDetails);
            }
        }
    }
}
