using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using M.Data;
using M.Models.HomeViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using M.Models;

namespace M.Controllers {
	public class HomeController : Controller {
		private MDbContext _db = null;
		//private readonly UserManager<User> _userManager;
		//private readonly SignInManager<User> _signInManager;

		public HomeController(MDbContext db, UserManager<User> userManager, SignInManager<User> signInManager) {
			_db = db;
			//_userManager = userManager;
			//_signInManager = signInManager;
		}
		
		public async Task<IActionResult> Index() {
			HomeViewModel model = new HomeViewModel();

			model.Posts = await _db.Posts.OrderByDescending(p => p.PostingTime).ToListAsync();

			return View(model);
		}
	}
}
