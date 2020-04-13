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
                .Handle(new GetOrderDetailQuery(11036, 59, false), CancellationToken.None)
                .Result;

            var expected = new GetOrderDetailForDisplayResponse()
            {
                CompanyName = "Drachenblut Delikatessen",
                EmployeeFirstName = "Laura",
                EmployeeLastName = "Callahan",
                OrderDate = new DateTime(1998, 4, 20),
                Quantity = 30,
                RequiredDate = new DateTime(1998, 5, 18),
                ShipAddress = "Walserweg 21",
                ShipCity = "Aachen",
                ShipCountry = "Germany",
                ShippedDate = new DateTime(1998, 4, 22),
                ShipPostalCode = "52066",
                ShipRegion = null,
            };

            Assert.IsType<GetOrderDetailForDisplayResponse>(actual);
            Helper.DeepEquals(expected, actual);
        }

        [Fact]
        public async Task GetDisplayData_OrderDetailNotFound()
        {
            Task act() => new GetOrderDetailQueryHandler(_context).Handle(new GetOrderDetailQuery(11036, 599, false), CancellationToken.None);
            await Assert.ThrowsAsync<DomainBadRequestException>(act);
        }

        [Fact]
        public void GetEditData_Ok()
        {
            var actual =
                new GetOrderDetailQueryHandler(_context)
                .Handle(new GetOrderDetailQuery(11036, 59, true), CancellationToken.None)
                .Result;

            var expected = new GetOrderDetailsForEditResponse()
            {
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
                DropDownListData = new Dictionary<DropDownListIdentifier, Dictionary<object, string>>()
                {
                    { DropDownListIdentifier.Customers, _context.Customers.ToDictionary(x=> (object)x.CustomerId, x=> x.CompanyName) },
                    { DropDownListIdentifier.Employees, _context.Employees.ToDictionary(x=> (object)x.EmployeeId, x=> $"{x.FirstName} {x.LastName}") },
                }
            };

            Assert.IsType<GetOrderDetailsForEditResponse>(actual);
            Helper.DeepEquals(expected, actual);
        }

        [Fact]
        public async Task GetEditData_OrderDetailNotFound()
        {
            Task act() => new GetOrderDetailQueryHandler(_context).Handle(new GetOrderDetailQuery(11036, 599, true), CancellationToken.None);
            await Assert.ThrowsAsync<DomainBadRequestException>(act);
        }
    }
}
