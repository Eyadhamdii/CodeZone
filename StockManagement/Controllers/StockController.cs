using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StockManagement.Controllers
{
    public class StockController : Controller
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> IncreaseStock()
        {
            var stores = await _mediator.Send(new GetAllStoresQuery());
            var items = await _mediator.Send(new GetAllItemsQuery());

            ViewBag.Stores = stores;
            ViewBag.Items = items;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseStock(IncreaseStockCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            await _mediator.Send(command);
            return Json(new { success = true, message = "Stock increased successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentQuantity(int storeId, int itemId)
        {
            if (storeId <= 0 || itemId <= 0)
                return BadRequest("Invalid store or item.");

            var currentQuantity = await _mediator.Send(new GetCurrentQuantityQuery
            {
                StoreId = storeId,
                ItemId = itemId
            });

            if (currentQuantity == null)
                return NotFound("Quantity not found.");

            return Ok(new { quantity = currentQuantity });
        }

    }
}
