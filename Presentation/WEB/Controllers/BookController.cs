using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace WEB.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _service;

		public BookController(IBookService service)
        {
            _service = service;
			
		}
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {

			string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			//if (string.IsNullOrEmpty(userId))
			//{
			//	return RedirectToAction("Login", "AppUser");
			//}
			PaginationVM<Book> vm = await _service.GetBooksCreatedByUserAsync(userId,page, take);
            if (vm.Items == null) return NotFound();
            return View(vm);
        }
		public async Task<IActionResult> CategoryBooks(int id, int page = 1, int take = 5)
		{
			PaginationVM<Book> book = await _service.GetCategoryId(id, page, take);
			book.CategoryId = id;
			if (book.Items == null) return NotFound();
			return View(book);
		}
		//public async Task<IActionResult> Index()
		//{
		//	var userBooks = await _service.GetUserBooksAsync();
		//	return View(userBooks);
		//}
		public async Task<IActionResult> Create()
        {
			BookCreateVM vm = new BookCreateVM();
			vm = await _service.CreatedAsync(vm);
			return View(vm);
		}
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateVM vm)
        {
			if (await _service.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index));
			return View(await _service.CreatedAsync(vm));
		}
		public async Task<IActionResult> Delete(int id)
		{
			if (await _service.DeleteAsync(id))
				return RedirectToAction(nameof(Index));
			return NotFound();
		}
		public async Task<IActionResult> Update(int id)
		{
			BookUpdateVM vm = new BookUpdateVM();
			vm = await _service.UpdatedAsync(id, vm);
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id, BookUpdateVM vm)
		{
			
			if (await _service.UpdateAsync(id, vm, ModelState))

				return RedirectToAction(nameof(Index));
			return View(await _service.UpdatedAsync(id, vm));
		}
		public async Task<IActionResult> Detail(int id)
		{
			return View(await _service.DetailAsync(id,User.Identity.Name));
		}
	}
}
