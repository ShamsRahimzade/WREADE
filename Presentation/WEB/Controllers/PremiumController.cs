using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Persistence.Implementations.Services;

namespace WEB.Controllers
{
	public class PremiumController : Controller
	{
		private readonly IUserService _service;

		public PremiumController(IUserService service)
        {
			_service = service;
		}
       
		public async Task<IActionResult> Premium(string userId)
		{
			var result = await _service.UpgradeToPremiumAsync(userId);
			if (result)
			{
				TempData["Message"] = "Upgrade to premium successful.";
			}
			else
			{
				TempData["Message"] = "Failed to upgrade to premium.";
			}
			return RedirectToAction("premium", "premium");
		}

		public async Task<IActionResult> DowngradeFromPremium(string userId)
		{
			var result = await _service.DowngradeFromPremiumAsync(userId);
			if (result)
			{
				TempData["Message"] = "Downgrade from premium successful.";
			}
			else
			{
				TempData["Message"] = "Failed to downgrade from premium.";
			}
			return RedirectToAction("Index", "Home");
		}
	}
}
