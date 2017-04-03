namespace M.Models.AccountViewModels {
	public class RegisterViewModel { 
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }

		public string FirstNameErrorMessage { get; set; }
		public string LastNameErrorMessage { get; set; }
		public string EmailErrorMessage { get; set; }
		public string PasswordErrorMessage { get; set; }
	}
}
