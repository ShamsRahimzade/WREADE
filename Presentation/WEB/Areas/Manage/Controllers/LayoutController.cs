using Microsoft.AspNetCore.Mvc;
using Stripe.Tax;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;

namespace WEB.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class LayoutController : Controller
    {
        private readonly ILayoutService _layoutService;

        public LayoutController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _layoutService.GetSettingsAsync();
            return View(settings);
        }
        public IActionResult Update(string key)
        {
            UpdateSettingsVM vm = new UpdateSettingsVM();
            return View(vm);
        }
		[HttpPost]
		public async Task<IActionResult> Update(string key, UpdateSettingsVM vm)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(vm.Value))
			{
				return BadRequest(); 
			}

			var result = await _layoutService.UpdateSettingAsync(key, vm);

			if (!result)
			{
				return NotFound(); 
			}

			return RedirectToAction(nameof(Index)); 
		}
	}
}
