using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class IncreaseStockCommand : IRequest
    {
        public int StoreId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
        public class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand>
        {
            private readonly AppDbContext _context;

            public IncreaseStockCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
            {
                var stock = await _context.Stock
                    .Where(s => s.StoreId == request.StoreId && s.ItemId == request.ItemId).FirstOrDefaultAsync(cancellationToken);

                if (stock == null)
                {
                    stock = new Stock
                    {
                        StoreId = request.StoreId,
                        ItemId = request.ItemId,
                        Quantity = request.Quantity
                    };
                    _context.Stock.Add(stock);
                }
                else
                {
                    stock.Quantity += request.Quantity;
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
