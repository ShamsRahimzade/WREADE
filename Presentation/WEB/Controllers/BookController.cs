using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;
using Wreade.Persistence.DAL;

namespace WEB.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _service;
		private readonly AppDbContext _context;

		public BookController(IBookService service, AppDbContext context)
        {
            _service = service;
			_context = context;
		}
		//[Authorize(Roles = "Writer,Admin")]

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
		[Authorize(Roles = "Writer,Reader,Admin")]

		public async Task<IActionResult> CategoryBooks(int id, int page = 1, int take = 5)
		{
			PaginationVM<Book> book = await _service.GetCategoryId(id, page, take);
			book.CategoryId = id;
			if (book.Items == null) return NotFound();
			return View(book);
		}
		[Authorize(Roles = "Writer,Reader,Admin")]

		public async Task<IActionResult> AllBook(int? chapterId, int? categoryId, int? order, int chaptercountFrom = 0, int chaptercountTo = 100, int page = 1, int take = 5)
		{
			IQueryable<Book> query = _context.Books.Include(j => j.Libraries).Include(j => j.BookCategories).ThenInclude(j => j.Category).Include(c => c.Chapters).AsQueryable();
			IQueryable<Category> querya = _context.Categories.AsQueryable();
			IQueryable<BookCategory> quaryc = _context.BookCategory.Include(cc => cc.Category).AsQueryable();
			switch (order)
			{
				case 1:
					query = query.OrderBy(j => j.Name);
					break;
				case 2:
					query = query.OrderByDescending(j => j.Chapters.Count);
					break;
				case 3:
					query = query.OrderByDescending(j => j.Id);
					break;
			}
			if (categoryId != null)
			{
				query = query.Where(c => c.BookCategories.FirstOrDefault(cc => cc.Category.Id == categoryId).CategoryId == categoryId);
			}
			
			// AllJob metodu içinde
			var selectedCities = Request.Query["selectedCities"].ToString(); 

			//if (!string.IsNullOrEmpty(selectedCities))
			//{
			//	var selectedCityList = selectedCities.Split(',').ToList();
			//	query = query.Where(c => c.Company.CompanyCities.Any(cc => selectedCityList.Contains(cc.City.Name)));
			//}
			//// AllJob metodu içinde
			//var uniqueCityNames = await _context.Cities.Select(c => c.Name).Distinct().ToListAsync();
			//ViewBag.UniqueCityNames = uniqueCityNames;


			

			//var allJobResult = await _service.AllBookAsync(page, take);

			//if (allJobResult != null)
			//{
			//	var model = new AllBookVM
			//	{
			//		Jobs = await query.ToListAsync(),

			//		CompanyCities = await quaryc.ToListAsync(),
			//		Categories = await querya.ToListAsync(),
			//		Companies = allJobResult.Companies,
			//		RangeValue = rangeValue,
			//		PriceFrom = priceFrom,
			//		PriceTo = priceTo
			//	};

				return View();
			//}
			//else
			//{
			//	return NotFound();
			//}
		}
		[Authorize(Roles = "Writer,Reader,Admin")]

		public async Task<IActionResult> TagBooks(int id, int page = 1, int take = 5)
		{
			PaginationVM<Book> book = await _service.GetTagId(id, page, take);
			book.CategoryId = id;
			if (book.Items == null) return NotFound();
			return View(book);
		}
		//public async Task<IActionResult> Index()
		//{
		//	var userBooks = await _service.GetUserBooksAsync();
		//	return View(userBooks);
		//}
		//[Authorize(Roles = "Writer,Admin")]

		public async Task<IActionResult> Create()
        {
			BookCreateVM vm = new BookCreateVM();
			vm = await _service.CreatedAsync(vm);
			return View(vm);
		}
        [HttpPost]
		//[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Create(BookCreateVM vm)
        {
			if (await _service.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index));
			return View(await _service.CreatedAsync(vm));
		}
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Delete(int id)
		{
			if (await _service.DeleteAsync(id))
				return RedirectToAction(nameof(Index));
			return NotFound();
		}
		[Authorize(Roles = "Writer,Admin")]

		public async Task<IActionResult> Update(int id)
		{
			BookUpdateVM vm = new BookUpdateVM();
			vm = await _service.UpdatedAsync(id, vm);
			return View(vm);
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Update(int id, BookUpdateVM vm)
		{
			
			if (await _service.UpdateAsync(id, vm, ModelState))

				return RedirectToAction(nameof(Index));
			return View(await _service.UpdatedAsync(id, vm));
		}
		[Authorize(Roles = "Writer,Reader,Admin")]

		public async Task<IActionResult> Detail(int id)
		{
			return View(await _service.DetailAsync(id,User.Identity.Name));
		}
	}
}
