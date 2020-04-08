using domain.Entities;
using FluentValidation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using app.Common;
using app.Common.Constants;

namespace app.Operations.Product.Commands.EditProduct
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public INorthwindContext _context;

        public EditProductCommandValidator(INorthwindContext context)
        {
            _context = context;

            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .MinimumLength(4).WithMessage(ValidationErrorMessages.WrongSize)
                .Matches("^[A-Za-z'_ öä]*$").WithMessage(ValidationErrorMessages.WrongFormat)
                .MustAsync(BeUnique).WithMessage(ValidationErrorMessages.AlreadyExists)
                ;

            RuleFor(x => x.SupplierId)
                .NotNull().WithMessage(ValidationErrorMessages.RequiredMessage)
                ;

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage(ValidationErrorMessages.RequiredMessage)
                ;

            RuleFor(x => x.QuantityPerUnit)
                .NotNull().WithMessage(ValidationErrorMessages.RequiredMessage)
                ;

            RuleFor(x => x.UnitPrice).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must(x => x.Value % 10 == 0 && x.Value > 0).WithMessage(ValidationErrorMessages.MustBeDevidableBy10AndGreaterThan0)
                ;

            RuleFor(x => x.UnitsInStock).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must((cmd, x) => !cmd.UnitsOnOrder.HasValue || (x > 0 && x >= cmd.UnitsOnOrder)).WithMessage(ValidationErrorMessages.UnitsInStockShouldBePositiveAndGreaterOrEqualUnitsOnOrder)
                ;

            RuleFor(x => x.UnitsOnOrder).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must((cmd, x) => !cmd.UnitsInStock.HasValue || (x > 0 && x <= cmd.UnitsInStock)).WithMessage(ValidationErrorMessages.UnitsInStockShouldBePositiveAndGreaterOrEqualUnitsOnOrder)
                ;

            RuleFor(x => x.ReorderLevel)
                .NotNull().WithMessage(ValidationErrorMessages.RequiredMessage)
                ;

            RuleFor(x => (int)x.ReorderLevel)
                .LessThan(5).WithMessage(ValidationErrorMessages.LesserThan)
                .When(x => x.ReorderLevel.HasValue)
                ;

            RuleFor(x => x.Discontinued)
                .NotNull().WithMessage(ValidationErrorMessages.RequiredMessage)
                ;
        }

        private async Task<bool> BeUnique(EditProductCommand editProductCommand, string productName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(productName))
                return true;

            return !await _context.Products
                .AnyAsync(l => l.ProductId != editProductCommand.ProductId.Value && l.ProductName.ToLower() == productName.ToLower());
        }
    }
}
