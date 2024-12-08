using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class UpdateItemCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, int>
        {
            private readonly AppDbContext _context;

            public UpdateItemCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
            {
                var store = await _context.Items
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
