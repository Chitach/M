using M.Data;
using M.Models;
using M.Models.ViewModels.Detail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace M.Controllers {
	public class DetailController : Controller {
		private MDbContext _db = null;
		//private readonly UserManager<User> _userManager;
		//private readonly SignInManager<User> _signInManager;

		public DetailController(MDbContext db, UserManager<User> userManager, SignInManager<User> signInManager) {
			_db = db;
			//_userManager = userManager;
			//_signInManager = signInManager;
		}

		public async Task<IActionResult> Index(int postId) {
			DetailViewModel model = new DetailViewModel();

			model.Post = _db.Posts.FirstOrDefault(p => p.Id == postId);

			if (model.Post == null) {
				return RedirectToAction("index", "home");
			}

			return View(model);
		}
	}
}
