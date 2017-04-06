using Microsoft.AspNetCore.Mvc;

namespace M.Controllers {
	public class ContactsController : Controller {
		[Route("contacts")]
		public IActionResult Index() {
			return View();
		}
	}
}
