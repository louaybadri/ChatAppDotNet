using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProjetDotNet.Data.Context;
using ProjetDotNet.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjetDotNet.Controllers
{
	public class MessengerController : Controller
	{
		private readonly ILogger<MessengerController> _logger;

		public MessengerController(ILogger<MessengerController> logger)
		{
			_logger = logger;
		}
		[HttpGet]
		[Route("/Messenger")]
		public IActionResult Index()
		{

			if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
			{
				return RedirectToAction("Login", "Connection");
			}
			else
			{

				UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
				return View("contact", unitOfWork.Users.GetAll().ToList());
			}
		}

		//[HttpGet]
		//[Route("/Messenger/{contactId}")]
		//public IActionResult SelectContact(int contactId)
		//{
		//	Debug.WriteLine(contactId);
		//	if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
		//	{
		//		RedirectToAction("Login", "Connection");
		//	}
		//	User currentUser = HttpContext.Session.GetString("currentUser").FromJson<User>();
		//	UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
		//	List<Message> conversation = unitOfWork.Messages.GetAllMessages(currentUser, contactId).ToList();
		//	ViewBag.Conversation = conversation;
		//	ViewBag.Users = ViewBag.Users;
		//	Debug.WriteLine("Here");
		//	Dictionary<string, object> data = new Dictionary<string, object>
		//	{
		//		{ "currentUser", currentUser },
		//		{ "conversation", conversation }
		//	};


		//	return View("Index", data);
		//}



		[HttpPost]
		[Route("/Messenger/{id}")]
		public IActionResult SendMessage(int id, string msgValue)
		{
			Debug.WriteLine(id);
			Debug.WriteLine(msgValue);
			//User currentUser= HttpContext.Session.GetString("currentUser").FromJson<User>();
			//Message message = new Message(currentUser.Id, id, msgValue, DateTime.Now, DateTime.Now);
			//UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
			//unitOfWork.Messages.Add(message);
			//unitOfWork.Complete();
			return View("index");
		}
		[Route("/Contact")]
		public IActionResult Contact()
		{
			if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
			{
				return RedirectToAction("Login", "Connection");
			}
			else
			{

				UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
				return View("contact", unitOfWork.Users.GetAll().ToList());
			}
		}
	}
}