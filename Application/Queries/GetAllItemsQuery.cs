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
    public class GetAllItemsQuery : IRequest<List<Item>>
    {
        public int Id { get; }

        public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, List<Item>>
        {
            private readonly AppDbContext _context;

            public GetAllItemsQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<List<Item>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Items.ToListAsync(cancellationToken);
            }
        }

    }
}
