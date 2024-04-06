using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Tax;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Application.ViewModels.Setting;
using Wreade.Domain.Entities;

namespace WEB.Areas.Manage.Controllers
{
    [Area("Manage")]
	[Authorize(Roles = "Admin")]


	public class LayoutController : Controller
    {
        private readonly ILayoutService _layoutService;

        public LayoutController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

		public async Task<IActionResult> Index(int page = 1, int take = 10)
		{
			PaginationVM<Setting> vm = await _layoutService.GetAllAsync(page, take);
			if (vm.Items == null) return NotFound();
			return View(vm);
		}
		public async Task<IActionResult> Create()
		{
			CreateSettingVM vm = new CreateSettingVM();
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateSettingVM vm)
		{
			if (await _layoutService.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index));
			return View(vm);
		}
		public async Task<IActionResult> Update(int id)
		{
			UpdateSettingsVM vm = new UpdateSettingsVM();
			vm = await _layoutService.UpdatedAsync(id, vm);
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id, UpdateSettingsVM vm)
		{
			if (await _layoutService.UpdateAsync(id, vm, ModelState))
				return RedirectToAction(nameof(Index));
			return View(await _layoutService.UpdatedAsync(id, vm));
		}
		public async Task<IActionResult> Delete(int id)
		{
			if (await _layoutService.DeleteAsync(id))
				return RedirectToAction(nameof(Index));
			return NotFound();
		}
	}
}
