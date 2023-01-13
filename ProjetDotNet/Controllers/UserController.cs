using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProjetDotNet.Data.Context;
using ProjetDotNet.Models;
using System.Net;
using System.Web.Helpers;
using System.Xml.Linq;

namespace ProjetDotNet.Controllers
{
	public class UserController : Controller
	{
		[HttpGet]
		[Route("/Profile")]
		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
			{
				return RedirectToAction("Login", "Connection");
			}
			else
			{
				return View("Index", HttpContext.Session.GetString("currentUser").FromJson<User>());
			}
		}

		[HttpPost]
		[Route("/Profile")]
		public IActionResult Index(string name, string email, string password, string address, string phone)
		{
			UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
			User user = HttpContext.Session.GetString("currentUser").FromJson<User>();
			user.Name = name;
			user.Email = email;
			user.Password = password;
			user.Phone = phone;
			user.Address = address;
			unitOfWork.Users.UpdateUser(user.Id, user);
			ViewBag.updateSuccess = unitOfWork.Complete();
			return View("Index", user);
		}
	}
}
