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
    public class CreateItemCommand : IRequest<int>
    {
        public string Name { get; set; }
        public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
        {
            private readonly AppDbContext _context;

            public CreateItemCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
            {
                var item = new Item
                {
                    Name = request.Name,
                };

                _context.Items.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return item.Id;
            }
        }
    }
}