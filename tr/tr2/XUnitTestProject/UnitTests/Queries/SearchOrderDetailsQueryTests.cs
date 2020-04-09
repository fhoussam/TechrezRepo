using app.Common;
using app.Operations.ProductOrders.Commands.EditOrderDetail;
using app.Operations.ProductOrders.Queries.SearchOrderDetails;
using AutoMapper;
using domain.Entities;
using NUnitTestProject;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace XUnitTestProject
{
    [Collection("Queries")]
    public class SearchOrderDetailsQueryTests
    {
        private readonly NorthwindContext _context;

        public SearchOrderDetailsQueryTests(QueriesServiceCollection queriesServiceCollection)
        {
            _context = queriesServiceCollection.context;
        }

        [Fact]
        public async Task SearchOrderDetailsQueryTestsResponse_Ok()
        {
            SearchOrderDetailsQuery searchOrderDetailsQuery = new SearchOrderDetailsQuery()
            {
                OrderDateFrom = new DateTime(1996,1,1),
                OrderDateTo = new DateTime(1997, 12, 30),
                PageIndex = 2,
                ProductId = 59,
            };

            var queryhandlerResult = await new SearchOrderDetailsQuery.SearchOrderDetailsQueryHandler(_context).Handle(searchOrderDetailsQuery, CancellationToken.None);
            Assert.Equal(PagerParams.PageSize, queryhandlerResult.Source.Count());
        }
    }
}
