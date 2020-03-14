using AutoMapper;
using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace app.Operations.Supplier.Queries
{
    public class GetAllSuppliersQuery :  IRequest<List<GetAllSuppliersResponse>>
    {
        public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, List<GetAllSuppliersResponse>>
        {
            private readonly INorthwindContext _context;
            private readonly IMapper _mapper;

            public GetAllSuppliersQueryHandler(INorthwindContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetAllSuppliersResponse>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
            {
                return await _context.Suppliers.ProjectTo<GetAllSuppliersResponse>(_mapper.ConfigurationProvider).ToListAsync();
            }
        }
    }
}
