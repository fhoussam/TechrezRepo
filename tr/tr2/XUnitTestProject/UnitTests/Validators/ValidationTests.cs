using app.Operations.ProductOrders.Commands.EditOrderDetail;
using app.Operations.ProductOrders.Queries.SearchOrderDetails;
using domain.Entities;
using MediatR;
using NUnitTestProject;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace XUnitTestProject.UnitTests.Validators
{
    public partial class ValidationTest
    {
        public static TheoryData<IValidationTester<IBaseRequest>> Data
            => new TheoryData<IValidationTester<IBaseRequest>>
        {
            new GenericValidationTester<EditOrderDetailCommand>(
                new EditOrderDetailCommand()
                {
                    CustomerId = "aaaa",
                    EmployeeId = 2,
                    OrderDate = DateTime.Now,
                    OrderId = 11030,
                    ProductId = 59,
                    Quantity = 300,
                    RequiredDate = DateTime.Now,
                    ShipAddress = "",
                    ShippedDate = DateTime.Now,
                    ShipPostalCode = "5455",
                },
                new EditOrderDetailCommandValidator(_context),
                new Dictionary<Expression<Func<EditOrderDetailCommand, object>>, ValidationErrorTypes>()
                {
                    { x=>x.RequiredDate, ValidationErrorTypes.PredicateValidator },
                    { x=>x.ShipPostalCode, ValidationErrorTypes.RegularExpressionValidator },
                    { x=>x.ShipAddress, ValidationErrorTypes.NotEmptyValidator },
                    { x=>x.Quantity, ValidationErrorTypes.AsyncPredicateValidator },
                    { x=>x.CustomerId, ValidationErrorTypes.LengthValidator },
                }
            ),
            //the 'Ok' test case
            new GenericValidationTester<EditOrderDetailCommand>
            (
                new EditOrderDetailCommand()
                {
                    CustomerId = "aaaaaaaavs",
                    EmployeeId = 2,
                    OrderDate = DateTime.Now,
                    OrderId = 11030,
                    ProductId = 59,
                    Quantity = 100,
                    RequiredDate = DateTime.Now.AddDays(7),
                    ShipAddress = "scsdcsdfsscsdcsdfsz sdcsdfsscsdcsdfsscsdcsdfsa dcsdfssc",
                    ShippedDate = DateTime.Now,
                    ShipPostalCode = "54559-3216",
                },
                new EditOrderDetailCommandValidator(_context)
            ),
            new GenericValidationTester<SearchOrderDetailsQuery>(
                new SearchOrderDetailsQuery(){ },
                new SearchOrderDetailsQueryValidator(),
                new Dictionary<Expression<Func<SearchOrderDetailsQuery, object>>, ValidationErrorTypes>()
                {
                    { x=>x.OrderDateFrom, ValidationErrorTypes.NotEmptyValidator },
                    { x=>x.OrderDateTo, ValidationErrorTypes.NotEmptyValidator },
                    { x=>x.ProductId, ValidationErrorTypes.NotEmptyValidator },
                }
            ),
            new GenericValidationTester<SearchOrderDetailsQuery>(
                new SearchOrderDetailsQuery()
                {
                    OrderDateFrom = DateTime.Now,
                    OrderDateTo = DateTime.Now.AddDays(-2),
                    ProductId = 59,
                },
                new SearchOrderDetailsQueryValidator(),
                new Dictionary<Expression<Func<SearchOrderDetailsQuery, object>>, ValidationErrorTypes>()
                {
                    { x=>x.OrderDateTo, ValidationErrorTypes.GreaterThanValidator },
                }
            ),
            new GenericValidationTester<SearchOrderDetailsQuery>(
                new SearchOrderDetailsQuery()
                {
                    OrderDateFrom = DateTime.Now,
                    OrderDateTo = DateTime.Now.AddDays(7),
                    ProductId = 59,
                },
                new SearchOrderDetailsQueryValidator()
            ),
        };

        public static readonly NorthwindContext _context = NorthwindContextFactory.Create();

        [Theory]
        [MemberData(nameof(Data))]
        public void EditOrderDetailCommandValidator_Ok(IValidationTester<IBaseRequest> commandTest)
        {
            var commandTestResult = commandTest.ValidationResultOk();
            Assert.Equal(commandTestResult.Expected, commandTestResult.Actual);
        }
    }
}
