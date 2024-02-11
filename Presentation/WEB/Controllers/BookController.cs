using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace WEB.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            PaginationVM<Book> vm = await _service.GetAllAsync(page, take);
            if (vm.Items == null) return NotFound();
            return View(vm);
        }
        public async Task<IActionResult> Create()
        {
			BookCreateVM vm = new BookCreateVM();
			vm = await _service.CreatedAsync(vm);
			return View(vm);
		}
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateVM vm)
        {
			if (await _service.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index));
			return View(await _service.CreatedAsync(vm));
		}
    }
}
