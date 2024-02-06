using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class DashBoard : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
