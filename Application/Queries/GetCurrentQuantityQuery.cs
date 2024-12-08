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
    public class GetCurrentQuantityQuery : IRequest<int?>
    {
        public int StoreId { get; set; }
        public int ItemId { get; set; }

        public class GetCurrentQuantityQueryHandler : IRequestHandler<GetCurrentQuantityQuery, int?>
        {
            private readonly AppDbContext _context;

            public GetCurrentQuantityQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int?> Handle(GetCurrentQuantityQuery request, CancellationToken cancellationToken)
            {
                return await _context.Stock
                    .Where(s => s.StoreId == request.StoreId && s.ItemId == request.ItemId)
                    .Select(s => s.Quantity)
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }

    }
}
