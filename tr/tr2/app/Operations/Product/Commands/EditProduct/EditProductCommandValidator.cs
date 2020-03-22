using domain.Entities;
using FluentValidation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace app.Operations.Product.Commands.EditProduct
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public INorthwindContext _context;

        public EditProductCommandValidator(INorthwindContext context)
        {
            _context = context;
            string isRequiredMessage = "{PropertyName} Is Mandatory";

            RuleFor(x => x.ProductId).NotNull().WithMessage("Product Id Should Be Provided, 0 for new, other than 0 for edit");

            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(isRequiredMessage)
                .MinimumLength(4).WithMessage("Product Name Wrong Size")
                .Matches("^[A-Za-z'_ öä]*$").WithMessage("Product Name Has Wrong Format")
                .MustAsync(BeUnique).WithMessage("Product Name Already Exists")
                ;

            RuleFor(x => x.SupplierId)
                .NotNull().WithMessage(isRequiredMessage)
                ;

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage(isRequiredMessage)
                ;

            RuleFor(x => x.QuantityPerUnit)
                .NotNull().WithMessage(isRequiredMessage)
                ;

            RuleFor(x => x.UnitPrice).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(isRequiredMessage)
                .Must(x => x.Value % 10 == 0 && x.Value > 0).WithMessage("Must Be Dividable By 10 And Greater Than 0")
                ;

            RuleFor(x => x.UnitsInStock).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(isRequiredMessage)
                .Must((cmd, x) => !cmd.UnitsOnOrder.HasValue || (x > 0 && x >= cmd.UnitsOnOrder)).WithMessage("Units In Stock Should Be Positive And Greater (Or Equal) Than Units On Order")
                ;

            RuleFor(x => x.UnitsOnOrder).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(isRequiredMessage)
                .Must((cmd, x) => !cmd.UnitsInStock.HasValue || (x > 0 && x <= cmd.UnitsInStock)).WithMessage("Units On Order Should Be Positive And Less (Or Equal) Than Units On Order")
                ;

            RuleFor(x => x.ReorderLevel)
                .NotNull().WithMessage(isRequiredMessage)
                ;

            RuleFor(x => (int)x.ReorderLevel)
                .LessThan(5).WithMessage("ReorderLevel Must Be Less Than 5 {ComparisonProperty}")
                .When(x => x.ReorderLevel.HasValue)
                ;

            RuleFor(x => x.Discontinued)
                .NotNull().WithMessage(isRequiredMessage)
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
