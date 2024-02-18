using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;

namespace WEB.Controllers
{
	public class LibraryController : Controller
	{
		private readonly ILibraryService _service;

		public LibraryController(ILibraryService service)
        {
			_service = service;
		}
		public async Task<IActionResult> Index(string userId)
		{
			var wishlist = await _service.GetLibraryAsync(userId);
			return View(wishlist);
		}

		[HttpPost]
		public async Task<IActionResult> AddToLibrary(int bookId,string userId)
		{
			await _service.AddToLibraryAsync(bookId, userId);
			return RedirectToAction("Index", new { userId });
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromLibrary(int bookId,string userId)
		{
			await _service.RemoveFromLibraryAsync(bookId,userId);
			return RedirectToAction("Index", new { userId });
		}
	}
}
