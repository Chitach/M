using Microsoft.AspNetCore.Mvc;

namespace M.Controllers {
	public class AboutAuthorController : Controller {
		[Route("author")]
		public IActionResult Index() {
			return View();
		}
	}
}
