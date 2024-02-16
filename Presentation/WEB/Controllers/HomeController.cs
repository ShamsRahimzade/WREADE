using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;
using Wreade.Persistence.DAL;
using Wreade.Persistence.Implementations.Services;

namespace WEB.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHomeService _service;
		private readonly IBookService _book;
		private readonly ICategoryService _categoryService;

		public HomeController(IHomeService service, IBookService book,ICategoryService categoryService)
		{
			_service = service;
			_book = book;
			_categoryService = categoryService;
		}

		public async Task<IActionResult> Index(string UserName)
		{
			HomeVM vm = await _service.GetAllAsync();
			ICollection<Book> readingHistory =await _book.GetAll();
			ICollection<Category> categories = await _categoryService.GetAll();
			
			var mostReadCategory = readingHistory .OrderBy(g => g.Rating) .Take(3).ToList();

			var viewModel = new HomeVM
			{
				
				UserName = UserName,
				MostReadCategory = categories.OrderBy(x => x.Rating).ToList(),
				ReadingHistory = readingHistory,
				Books = vm.Books,
				Users=vm.Users
			};

			return View(viewModel);
		}

	}



}