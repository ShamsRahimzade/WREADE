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
			vm.bookId = bookId; 
			vm = await _chapservice.CreatedAsync(vm);
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateChapterVM vm)
		{
			if (await _chapservice.CreateAsync(vm, ModelState))
				return RedirectToAction(nameof(Index), new { id = vm.bookId }); 
			return View(await _chapservice.CreatedAsync(vm));
		}
		public async Task<IActionResult> Delete(int id)
		{
			if (await _chapservice.DeleteAsync(id))
				return RedirectToAction(nameof(Index));
			return NotFound();
		}
		public async Task<IActionResult> Update(int id)
		{
			UpdateChapterVM vm = new UpdateChapterVM();
			vm = await _chapservice.UpdatedAsync(id, vm);
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id, UpdateChapterVM vm,string? returnurl)
		{
			if(returnurl is null) return RedirectToAction("Index","Home");
			if (await _chapservice.UpdateAsync(id, vm, ModelState))

				return Redirect(returnurl);
			return View(await _chapservice.UpdatedAsync(id, vm));
		}
		public async Task<IActionResult> LikePost(int chapid,string? returnUrl)
		{
			if (returnUrl is null) return RedirectToAction("Index", "Book");
			await _chapservice.LikeChapter(chapid);

			return Redirect(returnUrl);
		}
		public async Task<IActionResult> UnlikePost(int chapid, string? returnUrl)
		{
			if (returnUrl is null) return RedirectToAction("Index", "Book");
			await _chapservice.UnlikeChap(chapid);

			return Redirect(returnUrl);
		}
	}
}
