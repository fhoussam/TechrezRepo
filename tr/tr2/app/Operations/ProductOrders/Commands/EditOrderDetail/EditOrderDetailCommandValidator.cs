using FluentValidation;
using app.Common.Constants;
using System.Threading.Tasks;
using app.Operations.ProductOrders.Queries.IsQuantityValidQuery;
using static app.Operations.ProductOrders.Queries.IsQuantityValidQuery.IsQuantityValidQuery;
using System.Threading;
using domain.Entities;

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
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);

            RuleFor(x => x.CustomerId).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Length(3, 100).WithMessage(ValidationErrorMessages.LengthError);

            RuleFor(x => x.ShipAddress).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Length(10, 300).WithMessage(ValidationErrorMessages.LengthError);

            RuleFor(x => x.ShipPostalCode).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Matches("[0-9]{5}-[0-9]{4}").WithMessage(ValidationErrorMessages.WrongFormat);

            RuleFor(x => x.RequiredDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .GreaterThan(x => x.OrderDate).When(x => x.OrderDate.HasValue).WithMessage(ValidationErrorMessages.LesserThan)
                .LessThan(x => x.ShippedDate).When(x => x.ShippedDate.HasValue).WithMessage(ValidationErrorMessages.LesserThan);

            RuleFor(x => x.Quantity).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .MustAsync((q, v, ct) => isQuantityValid(q.OrderId, q.ProductId, v)).WithMessage(ValidationErrorMessages.QuantityNotSufficient);
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

