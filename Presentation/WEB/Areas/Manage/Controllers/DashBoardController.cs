using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles ="Admin")]

	public class DashBoard : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
