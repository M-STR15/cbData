using cbData.BE.DB.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace cbData.BE.DB.DataContext
{
	public class CbDataDbContext : DbContext
	{
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Product> Products { get; set; }

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

			#region DEBUG
			insertTestData(modelBuilder);
			#endregion
		}


		private void insertTestData(ModelBuilder modelBuilder)
		{
			var countProducts = 100;
			var countOrders = 200;
			insertTestData_Products(modelBuilder, countProducts);
			insertTestData_Orders(modelBuilder, countProducts, countOrders);
		}

		private void insertTestData_Products(ModelBuilder modelBuilder, int countProducts)
		{
			for (int i = 1; i < countProducts; i++)
			{
				var quantityRandom = new Random().Next(0, countProducts);
				var productName = $"Product {i}";
				modelBuilder.Entity<Product>().HasData(new Product(i, productName));
			}
		}

		private void insertTestData_Orders(ModelBuilder modelBuilder, int countProducts, int countOrders)
		{
			for (int i = 1; i < countOrders; i++)
			{
				var quantityRandom = new Random().Next(0, countOrders);
				var productIdRandom = new Random().Next(1, countProducts);
				modelBuilder.Entity<Order>().HasData(new Order(i, productIdRandom, quantityRandom, DateTime.UtcNow));
			}
		}
	}
}
