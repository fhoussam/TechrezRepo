using app.Common;
using app.Common.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Operations.ProductOrders.Queries.IsQuantityValidQuery
{
    public class IsQuantityValidQueryValidator : AbstractValidator<IsQuantityValidQuery>
    {
        public IsQuantityValidQueryValidator()
        {
            RuleFor(x => x.OrderID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ProductID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.Quantity).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
        }
    }
}
