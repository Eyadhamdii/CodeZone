using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StockManagement.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Index: Display all items
        public async Task<IActionResult> Index()
        {
            var items = await _mediator.Send(new GetAllItemsQuery());
            return View(items);
        }

        // Create: Display create form
        public IActionResult Create()
        {
            return View(new CreateItemCommand());
        }

        // Create (POST): Handle create form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateItemCommand command)
        {
            if (!ModelState.IsValid) return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        // Edit: Display edit form
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _mediator.Send(new GetItemByIdQuery(id));
            if (item == null)
                return NotFound();
            return View(item);
        }

        // Edit (POST): Handle edit form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateItemCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            if (!ModelState.IsValid) return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteItemCommand(id));

            return RedirectToAction(nameof(Index));
        }
    }
}
