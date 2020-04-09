using app.Common;
using domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Common.Constants;
using app.Operations.ProductOrders.Queries.SearchOrderDetails;
using static app.Operations.ProductOrders.Queries.SearchOrderDetails.SearchOrderDetailsQuery;
using static app.Operations.ProductOrders.Queries.IsQuantityValidQuery.IsQuantityValidQuery;
using System.Threading;
using app.Operations.ProductOrders.Queries.IsQuantityValidQuery;

namespace app.Operations.ProductOrders.Commands.EditOrderDetail
{
    public class EditOrderDetailCommandValidator : AbstractValidator<EditOrderDetailCommand>
    {
        private readonly INorthwindContext _context;

        public EditOrderDetailCommandValidator(INorthwindContext context)
        {
            _context = context;

            RuleFor(x => x.OrderId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.EmployeeID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.Quantity).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);

            RuleFor(x => x.OrderDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must(x => (DateTime.Now - x.Value).TotalDays <= 7).WithMessage(ValidationErrorMessages.DateShouldLessThanAWeek)
                //.Must((cmd, x)=> x.Value < cmd.RequiredDate).WithMessage("Order date should be greater than required date")
                .LessThan(x => x.RequiredDate).WithMessage(ValidationErrorMessages.LesserThan)
                ;

            RuleFor(x => x.RequiredDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must(x => DateTime.Now < x.Value).WithMessage(ValidationErrorMessages.DateBeNewerThanCurrent)
                //.Must((cmd, x) => cmd.OrderDate < x.Value).WithMessage("Order date should be greater than required date")
                .GreaterThan(x => x.OrderDate).WithMessage(ValidationErrorMessages.GreaterThan)
                ;

            RuleFor(x => x.ShippedDate).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ShipAddress).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);

            RuleFor(x => x.ShipPostalCode).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Matches("[0-9]{5}-[0-9]{4}").WithMessage(ValidationErrorMessages.WrongFormat)
                ;

            RuleFor(x => x.Quantity).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .MustAsync((q, v, ct) => isQuantityValid(q.OrderId, q.ProductId, v)).WithMessage(ValidationErrorMessages.QuantityNotSufficient)
                ;
        }

        private async Task<bool> isQuantityValid(int? orderId, int? productId, short? quantity)
        {
            var isQuantityValidQuery = new IsQuantityValidQuery() 
            { 
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
            };

            return await new IsQuantityValidQueryHandler(_context).Handle(isQuantityValidQuery, CancellationToken.None);
        }
    }
}
