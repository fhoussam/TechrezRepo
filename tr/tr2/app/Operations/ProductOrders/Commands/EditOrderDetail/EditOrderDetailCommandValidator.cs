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
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ShippedDate).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);

            RuleFor(x => x.CustomerId).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Length(8, 100).WithMessage(ValidationErrorMessages.LengthError)
                ;

            RuleFor(x => x.ShipAddress).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Length(50, 300).WithMessage(ValidationErrorMessages.LengthError)
                ;

            RuleFor(x => x.OrderDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must(x => (DateTime.Now - x.Value).TotalDays <= 7).WithMessage(ValidationErrorMessages.DateShouldLessThanAWeek)
                .LessThan(x => x.RequiredDate).WithMessage(ValidationErrorMessages.LesserThan)
                ;

            RuleFor(x => x.RequiredDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must(x => DateTime.Now < x.Value).WithMessage(ValidationErrorMessages.DateBeNewerThanCurrent)
                .GreaterThan(x => x.OrderDate).WithMessage(ValidationErrorMessages.GreaterThan)
                ;

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
