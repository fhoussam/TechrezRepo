using app.Operations.ProductOrders.Commands.EditOrderDetail;
using domain.Entities;
using MediatR;
using NUnitTestProject;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
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
                })
        };

        public static readonly NorthwindContext _context = NorthwindContextFactory.Create();

        [Theory]
        [MemberData(nameof(Data))]
        public void EditOrderDetailCommandValidator_Ok(IValidationTester<IBaseRequest> o)
        {
            o.ValidationResultOk();
        }
    }
}
