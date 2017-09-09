using M.Models;
using M.Models.AccountViewModels;
using M.Models.ValidationResultModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M.Controllers {
	public class AccountController : Controller {
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager) {
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		#region Register
		[HttpGet]
		public IActionResult Register() {
			RegisterViewModel model = new RegisterViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model) {
			if (IsRegModelValid(model)) {
				User user = new User { Email = model.Email, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName };
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded) {
					await _userManager.AddToRoleAsync(user, "user");

					// set cookies
					await _signInManager.SignInAsync(user, false);

					return Content("Success");
				}
			}
			return BadRequest("Validation error");
		}
		#endregion

		#region Login/Logout 
		[HttpGet]
		public IActionResult Login(string returnUrl = null) {
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model) {
			User user = await _userManager.FindByEmailAsync(model.Email);
			if (user != null) {
				var result =
					await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
				if (result.Succeeded) {
					// check if URL belongs to application
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) {
						return Redirect(model.ReturnUrl);
					} else {
						return RedirectToAction("Index", "Home");
					}
				} else {
					Response.StatusCode = 401;
					return Content("Wrong Password");
				}
			} else {
				Response.StatusCode = 402;
				return Content("User does not exist");
			}
		}

		public async Task<IActionResult> LogOff() {
			// delete identification cookies
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		#endregion

		#region private helpers
		private bool IsRegModelValid(RegisterViewModel model) {
			UserRegisterValidationResult result = new UserRegisterValidationResult();

			if (model.FirstName?.Trim()?.Length < 2) {
				return false;
			}

			if (model.LastName?.Trim()?.Length < 2) {
				return false;
			}

			if (string.IsNullOrEmpty(model.Email) || !Regex.IsMatch(model.Email, @"^(([^<>()\[\]\\.,;:\s@]+ (\.[^<>()\[\]\\.,;:\s@]+)*)|(.+))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", RegexOptions.IgnoreCase)) {
				return false;
			}

			if (model.Password != model.PasswordConfirm || model.Password?.Length < 8) {
				return false;
			} else {
				string reLetter = @"[a-z]";
				string reDigit = @"[0-9]";
				if (string.IsNullOrEmpty(model.Password) || (!Regex.IsMatch(model.Password, reLetter, RegexOptions.IgnoreCase) || !Regex.IsMatch(model.Password, reDigit, RegexOptions.IgnoreCase))) {
					return false;
				}
			}

			return true;
		}
		#endregion
	}
}
