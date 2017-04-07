using M.Models;
using M.Models.AccountViewModels;
using M.Models.ValidationResultModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M.Controllers {
	public class AccountController : Controller {
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager) {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region Register
		[HttpGet]
		public IActionResult Register() {
			RegisterViewModel model = new RegisterViewModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model) {
			UserRegisterValidationResult validationResults = ValidateUserRegisterModel(model);

			if (!validationResults.HasValidationErrors) {
				User user = new User { Email = model.Email, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName };
				// add user
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded) {
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
		private UserRegisterValidationResult ValidateUserRegisterModel(RegisterViewModel model) {
			UserRegisterValidationResult result = new UserRegisterValidationResult();

			if (model.FirstName.Trim().Length == 0) {
				result.FirstNameErrorMessage = "Ім\'я не може бути пустим";
				result.HasValidationErrors = true;
			}

			if (model.LastName.Trim().Length == 0) {
				result.LastNameErrorMessage = "Прізвище не може бути пустим";
				result.HasValidationErrors = true;
			}

			if (!Regex.IsMatch(model.Email, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.IgnoreCase)) {
				result.EmailErrorMessage = "Введіть коректний email. Приклад: Example@gmail.com";
				result.HasValidationErrors = true;
			}

			if (model.Password != model.PasswordConfirm) {
				result.PasswordErrorMessage = "Паролі не співпадають";
				result.HasValidationErrors = true;
			} else if (model.Password.Length < 8) {
				result.PasswordErrorMessage = "Пороль не повинен бути коротшим 8 символів";
				result.HasValidationErrors = true;
			} else {
				string reLetter = @"[a-z]";
				string reDigit = @"[0-9]";
				if (!Regex.IsMatch(model.Password, reLetter, RegexOptions.IgnoreCase) || !Regex.IsMatch(model.Password, reDigit, RegexOptions.IgnoreCase)) {
					result.PasswordErrorMessage = "Пароль повинен містити букви і цифри";
					result.HasValidationErrors = true;
				}
			}

			return result;
		}
		#endregion
	}
}
