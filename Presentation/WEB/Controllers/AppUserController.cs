using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.Abstractions.Services.MailService;
using Wreade.Application.ViewModels;
using Wreade.Application.ViewModels.Users;
using Wreade.Domain.Entities;
using Wreade.Persistence.Implementations.Services;

namespace WEB.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IUserService _service;
        private readonly UserManager<AppUser> _user;
		private readonly IMailService _mailService;

		public AppUserController(IUserService service, UserManager<AppUser> user, IMailService mailService)
        {
            _service = service;
            _user = user;
			_mailService = mailService;
		}
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            var combinevm = new IdentityVM
            {
                Register = register
            };
            if (await _service.Register(register, ModelState))
                return RedirectToAction("Index", "Home");
            return View(combinevm);
        }
        public IActionResult Login()
        {
            return View("register");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var combinevm = new IdentityVM
            {
                Login = login
            };
            if (await _service.Login(login, ModelState))
                return RedirectToAction("Index", "Home");

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
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await _user.FindByEmailAsync(vm.Email);
            if (user is null) return NotFound();
            string token = await _user.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action("ResetPassword", "AppUser", new { userId = user.Id, token = token }
            , HttpContext.Request.Scheme);
			await _mailService.SendEmailAsync(user.Email, "ResetPassword", link, false);
			return RedirectToAction(nameof(Login));
		}
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
            var user = await _user.FindByIdAsync(userId);
            if (user is null) return NotFound();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm, string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
            if (!ModelState.IsValid) return View(vm);
            var user = await _user.FindByIdAsync(userId);
            if (user is null) return NotFound();
            var identityUser = await _user.ResetPasswordAsync(user, token, vm.ConfirmPassword);

            return RedirectToAction(nameof(Login));
        }
    }
}
