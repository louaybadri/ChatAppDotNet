using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProjetDotNet.Controllers
{
	public class SignUpController : Controller
	{
		[HttpPost]
		public IActionResult Index(String Name, String Email, String Password, String Phone, String Address)
		{
			Debug.WriteLine("name {0} email {1} passwd {2} phone {3} add {4}");
			return View();
		}
	}
}
