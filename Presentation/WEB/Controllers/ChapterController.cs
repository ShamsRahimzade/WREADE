using Microsoft.AspNetCore.Mvc;
using Stripe;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace WEB.Controllers
{
    public class ChapterController : Controller
    {
		private readonly IChapterService _chapservice;

		public ChapterController(IChapterService chapservice)
        {
			_chapservice = chapservice;
		}
        public async Task< IActionResult> Index(int id,int page = 1, int take = 5)
        {
			PaginationVM<Chapter> vm = await _chapservice.GetAllAsync(id,page, take);
			vm.BookId = id;
			if (vm.Items == null) return NotFound();
			return View(vm);
		}
		public async Task<IActionResult> Create(int bookId)
		{
			CreateChapterVM vm = new CreateChapterVM();
			vm.bookId = bookId; // Kitabın ID'sini ViewModel'e aktarın
			vm = await _chapservice.CreatedAsync(vm);
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateChapterVM vm)
		{
			if (await _chapservice.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index), new { id = vm.bookId }); // Yeni oluşturulan chapter'ın kitap ID'siyle Index'e yönlendirin
			return View(await _chapservice.CreatedAsync(vm));
		}
	}
}
