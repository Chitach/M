using Microsoft.AspNetCore.Mvc;

namespace M.Controllers {
	public class AuthorController : Controller {
		[Route("author")]
		public IActionResult Index() {
			return View();
		}
	}
}
