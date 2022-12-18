using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProjetDotNet.Controllers
{
	public class LoginController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(string email, string password)
		{
			Debug.WriteLine("email {0} password {1}", email, password);
			return View();
		}


	}
}
