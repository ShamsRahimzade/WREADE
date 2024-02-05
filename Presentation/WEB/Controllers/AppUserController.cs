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
		public async Task<IActionResult> Register([FromForm] RegisterVM register)
		{
			var combinevm = new IdentityVM
			{
				Register = register
			};
			if (!ModelState.IsValid)
			{

				return View(combinevm);
			}
			var result = await _service.Register(register);
			if (result.Any())
			{
				foreach (var item in result)
				{
					ModelState.AddModelError(String.Empty, item);
					return View(combinevm);
				}
			}
			return RedirectToAction("Index", "Home");
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login([FromForm] LoginVM login)
		{
			var combinevm = new IdentityVM
			{
				Login = login
            };
            if (!ModelState.IsValid)
			{

				return View(combinevm);
			}
			var result = await _service.Login(login);
			if (result.Any())
			{
				foreach (var item in result)
				{
					ModelState.AddModelError(String.Empty, item);
					return View(combinevm);
				}
			}
			return RedirectToAction("Index", "Home");
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
