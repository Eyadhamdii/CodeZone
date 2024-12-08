using Application.Commands;
using Common;
using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetStoreByIdQuery : IRequest<UpdateStoreDTO>
    {
        public int Id { get; set; }

        public GetStoreByIdQuery(int id)
        {
            Id = id;
        }
        public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, UpdateStoreDTO>
        {
            private readonly AppDbContext _context;

            public GetStoreByIdQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<UpdateStoreDTO> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
            {
                var store = await _context.Stores
                    .Where(s => s.Id == request.Id)
                    .Select(x => new UpdateStoreDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).FirstOrDefaultAsync(cancellationToken);

                return store; 
            }
        }

    }
}
