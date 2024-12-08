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
    public class UpdateStoreCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand,int>
        {
            private readonly AppDbContext _context;

            public UpdateStoreCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
            {
                var store = await _context.Stores
                    .Where(s => s.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

                if (store == null)
                {
                    throw new Exception("Store not found");
                }

                store.Name = request.Name;
                await _context.SaveChangesAsync(cancellationToken);

                return store.Id; 
            }

           
        }
    }
}
