using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using M.Data;
using M.Models.HomeViewModels;
using Microsoft.EntityFrameworkCore;

namespace M.Controllers {
	public class HomeController : Controller {
		private MDbContext _db = null;
		
		public HomeController(MDbContext db) {
			_db = db;
		}
		
		public async Task<IActionResult> Index() {
			HomeViewModel model = new HomeViewModel();

			model.Posts = await _db.Posts.OrderByDescending(p => p.PostingTime).ToListAsync();

			return View(model);
		}
	}
}
