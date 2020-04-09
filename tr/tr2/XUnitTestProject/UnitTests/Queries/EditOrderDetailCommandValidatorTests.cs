using app.Operations.ProductOrders.Commands.EditOrderDetail;
using AutoMapper;
using domain.Entities;
using NUnitTestProject;
using System;
using Xunit;

namespace XUnitTestProject
{
    [Collection("Queries")]
    public class EditOrderDetailCommandValidatorTests
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public EditOrderDetailCommandValidatorTests(QueriesServiceCollection queriesServiceCollection)
        {
            _context = queriesServiceCollection.context;
            _mapper = queriesServiceCollection.mapper;
        }

        [Fact]
        public void EditOrderDetailCommandValidator_Ok()
        {
            EditOrderDetailCommand editOrderDetailCommand = new EditOrderDetailCommand()
            {
                CustomerID = "",
                EmployeeID = 2,
                OrderDate = DateTime.Now,
                OrderId = 11030,
                ProductId = 59,
                Quantity = 300,
                RequiredDate = DateTime.Now,
                ShipAddress = "",
                ShippedDate = DateTime.Now,
                ShipPostalCode = "5455",
            };

            EditOrderDetailCommandValidator validator = new EditOrderDetailCommandValidator(_context);
            var validationResult = validator.Validate(editOrderDetailCommand);
            Assert.Equal(5, validationResult.Errors.Count);
        }
    }
}
