using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;

namespace WEB.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _service;

		public CategoryController(ICategoryService service)
        {
			_service = service;
		}
        public async Task<IActionResult> AllCategory()
		{
			return View(await _service.GetAll());
		}
	}
}
