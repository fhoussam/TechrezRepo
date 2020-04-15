using app.Common.Constants;
using FluentValidation;

namespace app.Operations.ProductOrders.Queries.GetOrderDetails
{
    public class GetOrderDetailQueryValidator : AbstractValidator<GetOrderDetailQuery>
    {
        public GetOrderDetailQueryValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ForEdit).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
        }
    }
}
