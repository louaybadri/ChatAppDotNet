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
		[HttpPost]
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



		[HttpGet]
		[Route("/Messenger/{contactId}")]
		public IActionResult SendMessage([FromRoute] int contactId)
		{
			User currentUser = HttpContext.Session.GetString("currentUser").FromJson<User>();
			Debug.WriteLine(contactId);

			if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
			{
				return RedirectToAction("Login", "Connection");
			}
			else
			{
				UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
				User Receiver = unitOfWork.Users.Get(contactId);
				List<User> users = new List<User>
			{
					currentUser,
					Receiver
			};
				if (currentUser is null)
				{
					Debug.WriteLine("test test");
				}

				return View("index", new Tuple<List<User>, IEnumerable<Message>>(

					users, unitOfWork.Messages.GetAllMessages(currentUser, contactId)

					)
					);
			}
		}

		[HttpPost]
		[Route("/Messenger/{id}")]
		public IActionResult SendMessage([FromRoute] int id, string msgValue)
		{
			Debug.WriteLine(id);
			Debug.WriteLine(msgValue);
			User currentUser = HttpContext.Session.GetString("currentUser").FromJson<User>();
			Message message = new Message(currentUser.Id, id, msgValue, DateTime.Now, DateTime.Now);
			UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
			unitOfWork.Messages.Add(message);
			unitOfWork.Complete();
			Debug.WriteLine("message sent ");
			return RedirectToAction("SendMessage", "Messenger");
		}
	}
}