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
    public class DeleteItemCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteItemCommand(int id)
        {
            Id = id;
        }
        public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
        {
            private readonly AppDbContext _context;

            public DeleteItemCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
            {
                // Find the store to delete
                var item = await _context.Items
                    .Where(s => s.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

                if (item == null)
                {
                    throw new Exception("Store not found");
                }

                _context.Items.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
