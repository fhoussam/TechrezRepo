using AutoMapper;
using domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace app.Operations.Category.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<GetAllCategoriesResponse>>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<GetAllCategoriesResponse>>
        {
            private readonly INorthwindContext _context;
            private readonly IMapper _mapper;

            public GetAllCategoriesQueryHandler(INorthwindContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetAllCategoriesResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                return await _context.Categories.ProjectTo<GetAllCategoriesResponse>(_mapper.ConfigurationProvider).ToListAsync();
            }
        }
    }
}
