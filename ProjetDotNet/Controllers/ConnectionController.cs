using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ProjetDotNet.Data.Context;
using ProjetDotNet.Models;
namespace ProjetDotNet.Controllers
{
	public class ConnectionController : Controller
	{
		[HttpGet]
		[Route("/")]
		[Route("/Connection")]
		public IActionResult Login()
		{
			return View("Index");
		}

		[HttpPost]
		[Route("/")]
		[Route("/Connection")]
		public IActionResult Login(string email, string password)
		{
			UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
			User? user = unitOfWork.Users.Find(x => x.Email == email && x.Password == password).FirstOrDefault();
			return RedirectToAction("Index", "Messenger");
		}

		[HttpPost]
		[Route("/SignUp")]
		public IActionResult SignUp(string name, string email, string password, string type, string phone, string address)
		{
			UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
			int imageIndex = (unitOfWork.Users.GetAll().ToList().Count) % 16 + 1;
			String imageName = "https://mdbcdn.b-cdn.net/img/Photos/Avatars/avatar-" + imageIndex + ".webp";

			User user = new User(name, email, password, type, phone, address, imageName);
			if (user is not null)
			{
				if (unitOfWork.Users.Find(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault() is not null)
				{
					ViewBag.EmailFound = email;
					return View("Index");
				}
				else
				{
					unitOfWork.Users.Add(user);
				}
				unitOfWork.Complete();
			}
			return View("Index");
		}

		[HttpGet]
		[Route("/All")]
		public IActionResult All()
		{
			UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
			List<User> users = unitOfWork.Users.GetAll().ToList();
			return View(users);
		}

		//LogOut 
		[HttpPost]
		[Route("/LogOut")]
		public IActionResult LogOut()
		{
			//tfasa5 el session
			return View("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
