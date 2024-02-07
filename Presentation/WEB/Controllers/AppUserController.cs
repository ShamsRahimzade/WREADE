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
		public async Task<IActionResult> Register( RegisterVM register)
		{
			var combinevm = new IdentityVM
			{
				Register = register
			};
			if(await _service.Register(register, ModelState))
			return RedirectToAction("Index","Home");
			return View(combinevm);
		}
		public IActionResult Login()
		{
			return View("register");
		}
		[HttpPost]
		public async Task<IActionResult> Login( LoginVM login)
		{
			var combinevm = new IdentityVM
			{
				Login = login
            };
			if(await _service.Login(login, ModelState))
			return RedirectToAction("Index","Home");

			return View("register");
		}
		public async Task<IActionResult> Logout()
		{
			await _service.Logout();
			return RedirectToAction("Index", "Home");
		}
		public async Task<IActionResult> CreateRole()
		{
			await _service.CreateRoleAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
