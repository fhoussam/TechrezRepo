using app.Common;
using domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Common.Constants;

namespace app.Operations.ProductOrders.Commands.EditOrderDetail
{
    public class EditOrderDetailCommandValidator : AbstractValidator<EditOrderDetailCommand>
    {
        private readonly INorthwindContext _context;

        public EditOrderDetailCommandValidator(INorthwindContext context)
        {
            _context = context;

            RuleFor(x => x.OrderId).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ProductID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.CustomerID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.EmployeeID).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.Quantity).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);

            RuleFor(x => x.OrderDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must(x=> (DateTime.Now - x.Value).TotalDays <= 7).WithMessage(ValidationErrorMessages.DateShouldLessThanAWeek)
                //.Must((cmd, x)=> x.Value < cmd.RequiredDate).WithMessage("Order date should be greater than required date")
                .LessThan(x=>x.RequiredDate).WithMessage(ValidationErrorMessages.LesserThan)
                ;

            RuleFor(x => x.RequiredDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Must(x=> DateTime.Now < x.Value).WithMessage(ValidationErrorMessages.DateBeNewerThanCurrent)
                //.Must((cmd, x) => cmd.OrderDate < x.Value).WithMessage("Order date should be greater than required date")
                .GreaterThan(x => x.OrderDate).WithMessage(ValidationErrorMessages.GreaterThan)
                ;

            RuleFor(x => x.ShippedDate).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);
            RuleFor(x => x.ShipAddress).NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage);

            RuleFor(x => x.ShipPostalCode).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ValidationErrorMessages.RequiredMessage)
                .Matches("[0-9]{5}-[0-9]{4}").WithMessage(ValidationErrorMessages.WrongFormat)
                ;
        }

        private async Task<bool> isQuantityValid(int orderID, int productID, int quantity) 
        {
            var query = await (
                from od in _context.OrderDetails
                join p in _context.Products
                on od.ProductId equals p.ProductId
                where od.OrderId == orderID && p.ProductId == productID
                select new 
                { 
                    OldQuanity = od.Quantity,
                    UnitsInStock = p.UnitsInStock,
                }).ToListAsync();

            if(query.Count != 1)
                throw new Exception();

            return quantity < query[0].OldQuanity || quantity - query[0].OldQuanity <= query[0].UnitsInStock;
        }
    }
}
