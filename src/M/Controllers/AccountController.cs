using M.Models;
using M.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace M.Controllers {
	public class AccountController : Controller {
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager) {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register() {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model) {
			if (ModelState.IsValid) {
				User user = new User { Email = model.Email, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName };
				// add user
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded) {
					// set cookies
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				} else {
					foreach (var error in result.Errors) {
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}
	}
}
