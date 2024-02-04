using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Manage.Controllers
{
	public class DashBoard : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
