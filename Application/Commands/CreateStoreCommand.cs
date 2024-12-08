using Domain.Entities;
using Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateStoreCommand : IRequest<int>
    {
        public string Name { get; set; }

        public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, int>
        {
            private readonly AppDbContext _context;

            public CreateStoreCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
            {
                var store = new Store
                {
                    Name = request.Name,
                };

                _context.Stores.Add(store);
                await _context.SaveChangesAsync(cancellationToken);
                return store.Id;
            }
        }
    }
}
