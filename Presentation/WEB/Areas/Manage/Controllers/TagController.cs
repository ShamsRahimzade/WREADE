using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace WEB.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles = "Admin")]

	public class TagController : Controller
	{
		private readonly ITagService _service;

		public TagController(ITagService service)
		{
			_service = service;
		}
		public async Task<IActionResult> Index(int page = 1, int take = 5)
		{
			PaginationVM<Tag> vm;
			vm = await _service.GetAllAsync(page, take);
			if (vm.Items == null)
				return NotFound();
			return View(vm);
		}
		public async Task<IActionResult> Detail(int id)
		{
			return View(await _service.DetailAsync(id));
		}
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(TagCreateVM vm)
		{
			if (await _service.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index));
			return View(vm);
		}
		public async Task<IActionResult> Update(int id)
		{
			TagUpdateVM vm = new TagUpdateVM();
			vm = await _service.UpdatedAsync(id, vm);
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id, TagUpdateVM vm)
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
