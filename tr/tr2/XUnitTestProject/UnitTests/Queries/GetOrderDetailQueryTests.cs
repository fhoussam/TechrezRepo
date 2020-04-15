using app.Common.Enums;
using app.Common.Exceptions;
using app.Operations.ProductOrders.Queries.GetOrderDetails;
using domain.Entities;
using NUnitTestProject;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static app.Operations.ProductOrders.Queries.GetOrderDetails.GetOrderDetailQuery;
using app.Operations.Config.Commands;

namespace XUnitTestProject.UnitTests.Queries
{
    [Collection("Queries")]
    public class GetOrderDetailQueryTests
    {
        private readonly NorthwindContext _context;
        public GetOrderDetailQueryTests(QueriesServiceCollection queriesServiceCollection)
        {
            _context = queriesServiceCollection.context;
        }

        [Fact]
        public void GetDisplayData_Ok()
        {
            var actual =
                new GetOrderDetailQueryHandler(_context)
                .Handle(new GetOrderDetailQuery(59, 11036, false), CancellationToken.None)
                .Result;

            var expected = new GetOrderDetailsForDisplayResponse()
            {
                CompanyName = "Drachenblut Delikatessen",
                EmployeeFirstName = "Laura",
                EmployeeLastName = "Callahan",
                OrderDate = new DateTime(1998, 4, 20),
                Quantity = 30,
                OrderId = 11036,
                RequiredDate = new DateTime(1998, 5, 18),
                ShipAddress = "Walserweg 21",
                ShipCity = "Aachen",
                ShipCountry = "Germany",
                ShippedDate = new DateTime(1998, 4, 22),
                ShipPostalCode = "52066",
                ShipRegion = null,
            };

            Assert.IsType<GetOrderDetailsForDisplayResponse>(actual);
            Helper.DeepEquals(expected, actual);
        }

        [Fact]
        public async Task GetDisplayData_OrderDetailNotFound()
        {
            Task act() => new GetOrderDetailQueryHandler(_context).Handle(new GetOrderDetailQuery(599, 11036, false), CancellationToken.None);
            await Assert.ThrowsAsync<DomainBadRequestException>(act);
        }

        [Fact]
        public void GetEditData_Ok()
        {
            var actual =
                new GetOrderDetailQueryHandler(_context)
                .Handle(new GetOrderDetailQuery(59, 11036, true), CancellationToken.None)
                .Result;

            var expected = new GetOrderDetailsForEditResponse()
            {
                OrderId = 11036,
                OrderDate = new DateTime(1998, 4, 20),
                Quantity = 30,
                RequiredDate = new DateTime(1998, 5, 18),
                ShipAddress = "Walserweg 21",
                ShipCity = "Aachen",
                ShipCountry = "Germany",
                ShippedDate = new DateTime(1998, 4, 22),
                ShipPostalCode = "52066",
                ShipRegion = null,
                CustomerId = "DRACD",
                EmployeeId = 8,
                DropDownListData = new Dictionary<DropDownListIdentifier, IEnumerable<KeyValue>>()
                {
                    { DropDownListIdentifier.Customers, _context.Customers.Select(x=> new KeyValue(x.CustomerId, x.CompanyName)).ToList() },
                    { DropDownListIdentifier.Employees, _context.Employees.Select(x=> new KeyValue(x.EmployeeId, $"{x.FirstName} {x.LastName}")).ToList() },
                    { DropDownListIdentifier.Countries, _context.Customers.Select(x=> new KeyValue(x.Country, x.Country )) },
                }
            };

            Assert.IsType<GetOrderDetailsForEditResponse>(actual);
            Helper.DeepEquals(expected, actual);
        }

        [Fact]
        public async Task GetEditData_OrderDetailNotFound()
        {
            Task act() => new GetOrderDetailQueryHandler(_context).Handle(new GetOrderDetailQuery(599, 11036, true), CancellationToken.None);
            await Assert.ThrowsAsync<DomainBadRequestException>(act);
        }
    }
}
