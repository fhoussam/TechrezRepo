using app.Common;
using app.Common.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace app.Operations.ProductOrders.Commands.DeleteOrderDetail
{
    public class DeleteOrderDetailCommandValidator : AbstractValidator<DeleteOrderDetailCommand>
    {
        public DeleteOrderDetailCommandValidator()
        {
            RuleFor(x => x.OrderID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ProductID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
        }
    }
}
