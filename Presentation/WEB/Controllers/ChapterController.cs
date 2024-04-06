using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
		[Authorize(Roles = "Writer")]

		public async Task< IActionResult> Index(int bookId,int page = 1, int take = 5)
        {
			PaginationVM<Chapter> vm = await _chapservice.GetAllAsync(bookId, page, take);
			vm.BookId = bookId;
			if (vm.Items == null) return NotFound();
			return View(vm);
		}
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Create(int bookId)
		{
			CreateChapterVM vm = new CreateChapterVM();
			vm.bookId = bookId;
			
			vm = await _chapservice.CreatedAsync(vm);
			vm.bookId=bookId;
			return View(vm);
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Create(CreateChapterVM vm)
		{

			if (await _chapservice.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index), new { id = vm.bookId });
			return View(await _chapservice.CreatedAsync(vm));
		}
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Delete(int id)
		{
			if (await _chapservice.DeleteAsync(id))
				return RedirectToAction("Index","Chapter" );
			return NotFound();
		}
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Update(int id)
		{
			UpdateChapterVM vm = new UpdateChapterVM();
			vm = await _chapservice.UpdatedAsync(id, vm);
			return View(vm);
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> Update(int id, UpdateChapterVM vm)
		{
			
			if (await _chapservice.UpdateAsync(id, vm, ModelState))

				return RedirectToAction("Index", "Chapter");
			return View(await _chapservice.UpdatedAsync(id, vm));
		}
		
		public async Task<IActionResult> LikeChapter(int chapid)
		{
			
			await _chapservice.LikeChapter(chapid);

			return RedirectToAction();
		}
		public async Task<IActionResult> UnlikeChapter(int chapid, string? returnUrl)
		{
			if (returnUrl is null) return RedirectToAction("Index", "Book");
			await _chapservice.UnlikeChap(chapid);

			return Redirect(returnUrl);
		}
		[Authorize(Roles = "Reader,Writer,Admin")]

		public async Task<IActionResult> GetChapter(int id)
		{
			return View(await _chapservice.Chapter(id));
		}
		[Authorize(Roles = "Writer,Reader,Admin")]
		public async Task<IActionResult> GetAll(int bookid)
		{
			return View(await _chapservice.GetAll(bookid));
		}
		public async Task<IActionResult> Detail(int id,int bookid, int page = 1, int take = 1)
		{
			PaginationVM<Chapter> vm = await _chapservice.GetAllAsync(id, page, take);
			vm.BookId = bookid;

			if (vm.Items == null) return NotFound();
			return View(vm);
		}
		public async Task<IActionResult> ViewCount(int chapid)
		{
			return View(await _chapservice.ViewChapter(chapid));
		}
	}
}
