using app.Common.Exceptions;
using app.Operations.ProductOrders.Queries.GetOrderDetails;
using domain.Entities;
using NUnitTestProject;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using static app.Operations.ProductOrders.Queries.GetOrderDetails.GetOrderDetailQuery;
using Newtonsoft.Json.Linq;

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

            var companies = _context.Customers.Select(x => new { x.CustomerId, x.CompanyName }).ToDictionary(x=>x.CustomerId, x=>x.CompanyName);
            var employees = _context.Employees.Select(x => new { x.EmployeeId, FullName = x.FirstName + " " + x.LastName }).ToDictionary(x => x.EmployeeId, x => x.FullName);

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
                Companies = companies,
                Employees = employees,
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
