using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
