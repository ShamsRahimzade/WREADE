using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace WEB.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
		public async Task<IActionResult> Index(int page = 1, int take = 5)
		{
			PaginationVM<Category> vm;
				vm = await _service.GetAllAsync(page, take);
			if (vm.Items == null)
				return NotFound();
			return View(vm);
		}
		
		public async Task<IActionResult> Create()
        {
			return View();
        }
		[HttpPost]
		public async Task<IActionResult> Create(CategoryCreateVM vm)
		{
			if (await _service.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index));
			return View(vm);
		}
		public async Task<IActionResult> Update(int id)
		{
			CategoryUpdateVM vm = new CategoryUpdateVM();
			vm = await _service.UpdatedAsync(id, vm);
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id,CategoryUpdateVM vm)
		{
			if (await _service.UpdateAsync(id, vm, ModelState))
				return RedirectToAction(nameof(Index));
			return View(await _service.UpdatedAsync(id, vm));
		}
		public async Task<IActionResult> Delete(int id)
		{
			if (await _service.DeleteAsync(id))
				return RedirectToAction(nameof(Index));
			return NotFound();
		}
	}
}
