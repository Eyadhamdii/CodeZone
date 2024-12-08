using Common;
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
    public class GetItemByIdQuery : IRequest<UpdateStoreDTO>
    {
        public int Id { get; set; }

        public GetItemByIdQuery(int id)
        {
            Id = id;
        }
        public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, UpdateStoreDTO>
        {
            private readonly AppDbContext _context;

            public GetItemByIdQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<UpdateStoreDTO> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _context.Items
                    .Where(s => s.Id == request.Id)
                    .Select(x => new UpdateStoreDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).FirstOrDefaultAsync(cancellationToken);

                return item;
            }
        }

    }
}
