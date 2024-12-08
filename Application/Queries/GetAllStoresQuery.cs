using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAllStoresQuery : IRequest<List<Store>>
    {
        public class GetAllStoresQueryHandler : IRequestHandler<GetAllStoresQuery, List<Store>>
        {
            private readonly AppDbContext _context;

            public GetAllStoresQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<Store>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
            {
                return await _context.Stores.ToListAsync(cancellationToken);
            }
        }
    }
}
