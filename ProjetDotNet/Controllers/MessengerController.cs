using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using ProjetDotNet.Data.Context;
using ProjetDotNet.Models;
using System.Diagnostics;
//using System.Web.Mvc;

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

			//Session["test"] = "test";

			if (HttpContext.Session.GetString("currentUser") == "")
			{
				return Redirect("/Connection");
			}
			else
			{
				User currentUser = null;
				UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);
				List<User> users = unitOfWork.Users.GetAll().ToList();

				return View(new Tuple<User, List<User>>(currentUser, users));
			}
		}
		public JsonResult ConversationWithContact(int contact)
		{
			if (HttpContext.Session.GetString("currentUser") is null || HttpContext.Session.GetString("currentUser") == "")
			{
				return Json(new { status = "error", message = "User is not logged in" });
			}
			User currentUser = HttpContext.Session.GetString("currentUser").FromJson<User>();
			List<Message> conversations = new List<Message>();
			UnitOfWork unitOfWork = new UnitOfWork(ChatAppContext.Instance);


			return Json(
				new { status = "success", data = conversations },
				System.Web.Mvc.JsonRequestBehavior.AllowGet
			);
		}
	}
}