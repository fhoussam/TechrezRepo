using app.Common;
using app.Common.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Operations.ProductOrders.Queries.SearchOrderDetails
{
    public class SearchOrderDetailsQueryValidator : AbstractValidator<SearchOrderDetailsQuery>
    {
        public SearchOrderDetailsQueryValidator()
        {
            RuleFor(x => x.OrderDateFrom).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ProductId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);

            RuleFor(x => x.OrderDateTo).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .GreaterThan(x=>x.OrderDateFrom).WithMessage(ValidationErrorMessages.GreaterThan)
                ;
        }
    }
}
