using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace WEB.Controllers
{
    public class SearchController : Controller
    {
		private readonly ISearchService _service;

		public SearchController(ISearchService service)
        {
			_service = service;
		}
        public async Task<IActionResult> Search(string searchTerm,int? order)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 3)
            {
                TempData["ErrorMessage"] = "Search term must be at least 3 characters long.";
                return RedirectToAction(nameof(Index));
            }
            List<AppUser> users = await _service.GetUsers(searchTerm);
            List<Book> books = await _service.GetBooks(searchTerm);
            List<Category> categories = await _service.GetCategory(searchTerm);
            List<Tag> tags = await _service.GetTag(searchTerm);
			
			SearchVM vm = new SearchVM
            {
                books = books,
                Users = users,
                Categories = categories,
                Tags = tags
			};
			switch (order)
			{
				case 1:
					vm.Users = users;
					break;

				case 2:
                        vm.books = books;
					break;
				case 3:
					vm.Categories = categories;
					break;
				case 4:
					vm.Tags = tags;
					break;

			}
			return View(vm);
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
            List<Book> books = await _service.GetBooks(searchTerm);
            List<Category> categories = await _service.GetCategory(searchTerm);
            List<Tag> tags = await _service.GetTag(searchTerm);
            SearchVM vm = new SearchVM
            {
				books = books,
				Users = users,
				Categories = categories,
				Tags = tags
			};
            return View("Search", vm);
        }
    }
}