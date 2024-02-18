using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;

namespace WEB.Controllers
{
	public class LibraryController : Controller
	{
		private readonly ILibraryService _service;

		public LibraryController(ILibraryService service)
        {
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			return View( await _service.GetLibraryItem());
			
		}
		public async Task<IActionResult> AddLibrary(int bookid,string userId)
		{
			await _service.AddBasket(bookid,userId);
			return RedirectToAction("Index", "Library", new { id = bookid });
		}

	}
}
