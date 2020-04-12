using app.Operations.ProductOrders.Commands.DeleteOrderDetail;
using domain.Entities;
using NUnitTestProject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace XUnitTestProject.UnitTests.Commands
{
    [Collection("Queries")]
    public class DeleteOrderDetailCommandTests
    {
        private readonly NorthwindContext _context;
        public DeleteOrderDetailCommandTests(QueriesServiceCollection queriesServiceCollection)
        {
            _context = queriesServiceCollection.context;
        }

        [Fact]
        public async Task DeleteOrderDetailCommand_Ok() 
        {
            var command = new DeleteOrderDetailCommand() { OrderID = 111036, ProductID = 59 };
            var commandExcution = new DeleteOrderDetailCommand.DeleteOrderDetailCommandHandler(_context).Handle(command, CancellationToken.None);
            var entityExists = await _context.OrderDetails.AnyAsync(x => x.OrderId == command.OrderID && x.ProductId == command.ProductID);
            Assert.True(!entityExists);
        }
    }
}
