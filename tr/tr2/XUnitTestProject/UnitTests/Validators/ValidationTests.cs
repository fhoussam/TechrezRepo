using app.Common.Constants;
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
                new Dictionary<Expression<Func<EditOrderDetailCommand, object>>, ValidationErrorMessage>()
                {
                    { x=> x.CustomerId, new ValidationErrorMessage(ValidationErrorMessages.RequiredMessage, ValidationErrorTypes.NotEmptyValidator) },
                    { x=> x.ShipAddress, new ValidationErrorMessage(ValidationErrorMessages.RequiredMessage, ValidationErrorTypes.NotEmptyValidator) },
                    { x=> x.ShipPostalCode, new ValidationErrorMessage(ValidationErrorMessages.WrongFormat, ValidationErrorTypes.RegularExpressionValidator) },
                    { x=> x.Quantity, new ValidationErrorMessage(ValidationErrorMessages.QuantityNotSufficient, ValidationErrorTypes.AsyncPredicateValidator) },
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
                    RequiredDate = DateTime.Now.AddDays(7),
                    ShippedDate = DateTime.Now.AddDays(14),
                    OrderId = 11030,
                    ProductId = 59,
                    Quantity = 100,                    
                    ShipAddress = "scsdcsdfsscsdcsdfsz sdcsdfsscsdcsdfsscsdcsdfsa dcsdfssc",
                    ShipPostalCode = "54559-3216",
                },
                new EditOrderDetailCommandValidator(_context)
            ),
            //new GenericValidationTester<SearchOrderDetailsQuery>(
            //    new SearchOrderDetailsQuery(){ },
            //    new SearchOrderDetailsQueryValidator(),
            //    new Dictionary<Expression<Func<SearchOrderDetailsQuery, object>>, ValidationErrorMessage>()
            //    {
            //        { x=> x.OrderDateFrom, new ValidationErrorMessage(ValidationErrorMessages.RequiredMessage, ValidationErrorTypes.NotEmptyValidator) },
            //        { x=> x.OrderDateTo, new ValidationErrorMessage(ValidationErrorMessages.RequiredMessage, ValidationErrorTypes.NotEmptyValidator) },
            //        { x=> x.ProductId, new ValidationErrorMessage(ValidationErrorMessages.RequiredMessage, ValidationErrorTypes.NotEmptyValidator) },
            //    }
            //),
            //new GenericValidationTester<SearchOrderDetailsQuery>(
            //    new SearchOrderDetailsQuery()
            //    {
            //        OrderDateFrom = DateTime.Now,
            //        OrderDateTo = DateTime.Now.AddDays(-2),
            //        ProductId = 59,
            //    },
            //    new SearchOrderDetailsQueryValidator(),
            //    new Dictionary<Expression<Func<SearchOrderDetailsQuery, object>>, ValidationErrorTypes>()
            //    {
            //        { x=>x.OrderDateTo, ValidationErrorTypes.GreaterThanValidator },
            //    }
            //),
            //new GenericValidationTester<SearchOrderDetailsQuery>(
            //    new SearchOrderDetailsQuery()
            //    {
            //        OrderDateFrom = DateTime.Now,
            //        OrderDateTo = DateTime.Now.AddDays(7),
            //        ProductId = 59,
            //    },
            //    new SearchOrderDetailsQueryValidator()
            //),
        };

        public static readonly NorthwindContext _context = NorthwindContextFactory.Create();

        [Theory]
        [MemberData(nameof(Data))]
        public void EditOrderDetailCommandValidator_Ok(IValidationTester<IBaseRequest> commandTest)
        {
            var commandTestResult = commandTest.ValidationResultOk();
            Helper.DeepEquals(commandTestResult.Expected, commandTestResult.Actual);
        }
    }
}
