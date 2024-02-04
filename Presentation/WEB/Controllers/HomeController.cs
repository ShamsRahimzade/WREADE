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

		public HomeController(IHomeService service, IBookService book)
		{
			_service = service;
			_book = book;
		}

		public async Task<IActionResult> Index(string UserName)
		{
			HomeVM vm = await _service.GetAllAsync();
			var readingHistory = _book.GetReadingHistoryForUser(UserName);

			var mostReadCategory = readingHistory.GroupBy(book => book.BookCategories)
												 .OrderByDescending(group => group.Count())
												 .Select(group => group.Key)
												 .FirstOrDefault();

			var viewModel = new HomeVM
			{
				
				UserName = UserName,
				MostReadCategory = mostReadCategory,
				ReadingHistory = readingHistory,
				Books = vm.Books,
				Users = vm.Users
			};

			return View(viewModel);
		}

	}



}