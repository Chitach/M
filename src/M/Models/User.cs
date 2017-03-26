using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace M.Models {
	public class User : IdentityUser {
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
