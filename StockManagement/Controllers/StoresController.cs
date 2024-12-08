using Application.Commands;
using Application.Queries;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StockManagement.Controllers
{
    public class StoresController : Controller
    {
        private readonly IMediator _mediator;

        public StoresController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var stores = await _mediator.Send(new GetAllStoresQuery());
            return View(stores);
        }
        public IActionResult Create()
        {
            return View(new CreateStoreCommand());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStoreCommand command)
        {
            if (!ModelState.IsValid) return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _mediator.Send(new GetStoreByIdQuery(id));
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateStoreCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteStoreCommand(id)); 
            return RedirectToAction(nameof(Index));
        }
    }

}
