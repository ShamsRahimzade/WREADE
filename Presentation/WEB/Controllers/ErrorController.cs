using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult ErrorPage(string error)
		{
			return View(model: error);
		}
	}
}
