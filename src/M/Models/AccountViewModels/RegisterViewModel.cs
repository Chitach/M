using System.ComponentModel.DataAnnotations;

namespace M.Models.AccountViewModels {
	public class RegisterViewModel { 
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
	}
}
