using angularclient.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Validators
{
    public class ProductValidator : AbstractValidator<ProductPostSave>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(5).WithMessage("Invalid description length")
                .MinimumLength(5).WithMessage("Invalid description length")
                .When(x => x.Description == "SomeForbiddenDescription").WithMessage("Username or email are required");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .When(x => x.Quantity > 0).WithMessage("Quantity should be superior to 0");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required")
                .When(x => x.Price > 1).WithMessage("Price should be superior to 1");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required")
                ;
        }
    }
}
