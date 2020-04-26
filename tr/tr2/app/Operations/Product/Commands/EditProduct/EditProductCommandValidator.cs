using FluentValidation;
using app.Common.Constants;

namespace app.Operations.Product.Commands.EditProduct
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
        {
            RuleFor(x => x.SupplierId).NotNull().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.CategoryId).NotNull().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.QuantityPerUnit).NotNull().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.UnitPrice).NotNull().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.UnitsInStock).NotNull().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.UnitsOnOrder).NotNull().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ReorderLevel).NotNull().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.Discontinued).NotNull().WithMessage(ValidationErrorMessages.RequiredMessage);

            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .MinimumLength(4).WithMessage(ValidationErrorMessages.WrongSize)
                .Matches("^[A-Za-z'_ öä]*$").WithMessage(ValidationErrorMessages.WrongFormat)
                ;
        }
    }
}
