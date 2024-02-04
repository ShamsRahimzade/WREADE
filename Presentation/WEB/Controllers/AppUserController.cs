using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Application.ViewModels.Users;

namespace WEB.Controllers
{
	public class AppUserController : Controller
	{
		private readonly IUserService _service;

		public AppUserController(IUserService service)
        {
			_service = service;
		}
		public IActionResult Register()
		{
			return View();
		}
        [HttpPost]
		public async Task<IActionResult> Register([FromForm] RegisterVM vm)
		{
			await _service.Register(vm);
			return RedirectToAction("Index","Home");
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login([FromForm] LoginVM vm)
		{
			await _service.Login(vm);
			return RedirectToAction("Index", "Home");
		}
	}
}
