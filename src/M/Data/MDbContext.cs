using M.Models;
using Microsoft.EntityFrameworkCore;

namespace M.Data {
	public class MDbContext : DbContext {
		public DbSet<Post> Posts { get; set; }

		public MDbContext(DbContextOptions<MDbContext> options)
			: base(options) 
		{
		}
	}
}
