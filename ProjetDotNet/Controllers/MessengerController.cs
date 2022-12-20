using Microsoft.AspNetCore.Mvc;
using ProjetDotNet.Data.Context;
using ProjetDotNet.Models;
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
			User currentUser = null;
			// Session logic
			UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
			List<User> users = unitOfWork.Users.GetAll().ToList();

			return View(new Tuple<User, List<User>>(currentUser, users));
		}
	}
}