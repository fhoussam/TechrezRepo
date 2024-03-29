﻿using app.Common.Constants;
using FluentValidation;

namespace app.Operations.ProductOrders.Queries.IsQuantityValidQuery
{
    public class IsQuantityValidQueryValidator : AbstractValidator<IsQuantityValidQuery>
    {
        public IsQuantityValidQueryValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.Quantity).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
        }
    }
}
