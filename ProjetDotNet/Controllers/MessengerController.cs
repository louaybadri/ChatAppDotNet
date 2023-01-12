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
				IEnumerable<User> users = unitOfWork.Users.GetAll().ToList();
				users = users.Where(x => x.Id != HttpContext.Session.GetString("currentUser").FromJson<User>().Id);
				return View("contact", users);
			}
		}



		[HttpGet]
		[Route("/Messenger/{contactId}")]
		public IActionResult SendMessage([FromRoute] int contactId)
		{


			if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
			{
				return RedirectToAction("Login", "Connection");
			}
			else
			{
				User currentUser = HttpContext.Session.GetString("currentUser").FromJson<User>();
				UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
				User? Receiver = unitOfWork.Users.Get(contactId);
				if (Receiver == null)
				{
					return RedirectToAction("Index");
				}
				List<User> users = new List<User>
										{
												currentUser,
												Receiver
										};

				return View("index", new Tuple<List<User>, IEnumerable<Message>>(

					users, unitOfWork.Messages.GetAllMessages(currentUser, contactId)

					)
					);
			}
		}


		[Route("/Messenger/{id}")]
		public IActionResult DeleteMessage([FromRoute] int id, string msgValue)
		{
			if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
			{
				return RedirectToAction("Login", "Connection");
			}
			else
			{
				User currentUser = HttpContext.Session.GetString("currentUser").FromJson<User>();
				UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
				unitOfWork.Messages.Remove();
				unitOfWork.Complete();
				return RedirectToAction("SendMessage", "Messenger");
			}
		}

		[HttpPost]
		[Route("/Messenger/{id}")]
		public IActionResult SendMessage([FromRoute] int id, string msgValue)
		{
			if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
			{
				return RedirectToAction("Login", "Connection");
			}
			else
			{
				User currentUser = HttpContext.Session.GetString("currentUser").FromJson<User>();
				Message message = new Message(currentUser.Id, id, msgValue, DateTime.Now, DateTime.Now);
				UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
				unitOfWork.Messages.Add(message);
				unitOfWork.Complete();
				return RedirectToAction("SendMessage", "Messenger");
			}
		}
	}
}