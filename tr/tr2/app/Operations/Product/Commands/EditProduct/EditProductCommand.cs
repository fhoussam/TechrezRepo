using AutoMapper;
using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace app.Operations.Product.Commands.EditProduct
{
    public class EditProductCommand : IRequest<int>
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public class EditProductCommandHandler : IRequestHandler<EditProductCommand, int>
        {
            private readonly INorthwindContext _context;
            private readonly IMapper _mapper;
            public EditProductCommandHandler(INorthwindContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(EditProductCommand request, CancellationToken cancellationToken)
            {
                if (request.ProductId == null)
                {
                    var ToAdd = _mapper.Map<Products>(request);
                    await _context.Products.AddAsync(ToAdd);
                }
                else 
                {
                    var toEdit = await _context.Products.SingleOrDefaultAsync(x => x.ProductId == request.ProductId);
                    _mapper.Map(request, toEdit);
                    toEdit.ProductName = request.ProductName;
                    toEdit.SupplierId = request.SupplierId;
                    toEdit.CategoryId = request.CategoryId;
                    toEdit.QuantityPerUnit = request.QuantityPerUnit;
                    toEdit.UnitPrice = request.UnitPrice;
                    toEdit.UnitsInStock = request.UnitsInStock;
                    toEdit.UnitsOnOrder = request.UnitsOnOrder;
                    toEdit.ReorderLevel = request.ReorderLevel;
                    toEdit.Discontinued = request.Discontinued;
                }

                return await _context.SaveChangeAsyc();
            }
        }
    }
}
