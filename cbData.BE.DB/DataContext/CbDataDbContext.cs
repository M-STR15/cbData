using cbData.BE.DB.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace cbData.BE.DB.DataContext
{
	public class CbDataDbContext : DbContext
	{
		public virtual DbSet<Order> Orders { get; set; }

		public CbDataDbContext(DbContextOptions<CbDataDbContext> options) : base(options)
		{

		}

		public CbDataDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<CbDataDbContext>();

			// Connection string musí být stejný jako v DI
			var connectionString = "cbDataDb-local";
			optionsBuilder.UseSqlServer(connectionString);

			return new CbDataDbContext(optionsBuilder.Options);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema("dbo");

		}
	}
}
