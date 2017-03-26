using M.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace M.Data {
	public class MDbContext : IdentityDbContext<User> {
		public DbSet<Post> Posts { get; set; }

		public MDbContext(DbContextOptions<MDbContext> options)
			: base(options) 
		{
		}
	}
}
