namespace M.Models.ValidationResultModels
{
	public class UserRegisterValidationResult
	{
		public bool HasValidationErrors { get; set; }
		public string FirstNameErrorMessage { get; set; }
		public string LastNameErrorMessage { get; set; }
		public string EmailErrorMessage { get; set; }
		public string PasswordErrorMessage { get; set; }

		public string ServerErrorMessage { get; set; }
	}
}
