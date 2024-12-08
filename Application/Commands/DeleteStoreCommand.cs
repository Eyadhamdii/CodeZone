using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteStoreCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteStoreCommand(int id)
        {
            Id = id;
        }
        public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand>
        {
            private readonly AppDbContext _context;

            public DeleteStoreCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
            {
                // Find the store to delete
                var store = await _context.Stores
                    .Where(s => s.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

                if (store == null)
                {
                    throw new Exception("Store not found");
                }

                _context.Stores.Remove(store);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value; 
            }
        }
    }
}
