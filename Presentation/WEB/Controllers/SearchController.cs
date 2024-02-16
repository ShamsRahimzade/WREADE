using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Domain.Entities;

namespace WEB.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUserService _service;
        private readonly UserManager<AppUser> _user;

        public SearchController(IUserService service, UserManager<AppUser> user)
        {
            _service = service;
            _user = user;
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 3)
            {
                TempData["ErrorMessage"] = "Search term must be at least 3 characters long.";
                return RedirectToAction(nameof(Index));
            }
            List<AppUser> users = await _service.GetUsers(searchTerm);
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchBook(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 3)
            {
                TempData["ErrorMessage"] = "Search term must be at least 3 characters long.";
                return View("Search");
            }
            List<AppUser> users = await _service.GetUsers(searchTerm);
            return View("Search", users);
        }
    }
}
