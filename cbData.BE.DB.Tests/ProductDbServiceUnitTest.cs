#pragma warning disable IDE0058

using cbData.BE.DB.DataContext;
using cbData.BE.DB.Models.Products;
using cbData.BE.DB.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace cbData.BE.DB.Tests.Services
{
	public class ProductDbServiceTests
	{
		private readonly Mock<IDbContextFactory<CbDataDbContext>> _contextFactoryMock;
		private readonly ProductDbService _service;
		private readonly DbContextOptions<CbDataDbContext> _dbOptions;

		public ProductDbServiceTests()
		{
			_contextFactoryMock = new Mock<IDbContextFactory<CbDataDbContext>>();
			_dbOptions = new DbContextOptionsBuilder<CbDataDbContext>()
							.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unik�tn� DB pro ka�d� test
							.Options;
			_service = new ProductDbService(_contextFactoryMock.Object);
		}

		private async Task<CbDataDbContext> createContextAsync()
		{
			var context = new CbDataDbContext(_dbOptions);
			await context.Database.EnsureCreatedAsync();
			_contextFactoryMock.Setup(x => x.CreateDbContextAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(context);
			return context;
		}

		/// <summary>
		/// P�ed ka�d�m testem datab�zi explicitn� odstra�te a znovu vytvo�te
		/// </summary>
		/// <param name="context"></param>
		private async void resetDB(CbDataDbContext context)
		{
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();
		}

		/// <summary>
		/// Otestov�n� methody pro vkl�d�n� produktu do DB.
		/// </summary>
		[Fact]
		public async Task AddProductTestAsync()
		{
			await using var context = await createContextAsync();

			var product = new Product { Name = "Test Product", Description = "Test Description" };

			resetDB(context);

			var result = await _service.AddProductAsync(product);
			var resultDb = await _service.GetProductAsync(product.Id);

			Assert.NotNull(result);
			Assert.NotNull(resultDb);

			//otestov�n� zda se vlo�en� produkt shoduje s t�m co je v DB
			var ignoredProperties = new HashSet<string> { nameof(Product.Id) };
			testAllProperties<Product>(product, result, ignoredProperties);

			//otesov�n� zda se vr�cen� objekt co vystupuje z methody se shoduje s t�m co je v DB
			var ignoredProperties2 = new HashSet<string> { nameof(Product.Orders) };
			testAllProperties<Product>(result, resultDb, ignoredProperties2);
		}

		/// <summary>
		/// Otestov�n� methody pro p�id�n� objedn�vky do DB.
		/// </summary>
		[Fact]
		public async Task AddOrderTestAsync()
		{
			await using var context = await createContextAsync();
			resetDB(context);

			var product = new Product { Name = "Test Product", Description = "Test Description" };
			var productDb = await _service.AddProductAsync(product);
			Assert.NotNull(productDb);
			var order = new Order(productDb.Id, Random.Shared.Next());

			var result = await _service.AddOrderAsync(order);
			var resultDb = await _service.GetOrderAsync(order.Id);
		
			Assert.NotNull(result);
			Assert.NotNull(resultDb);

			var ignoredProperties = new HashSet<string> { nameof(Order.Product) };
			testAllProperties<Order>(result, resultDb, ignoredProperties);
		}

		private void testAllProperties<T>(T expectedObj, T actualObj, HashSet<string>? ignoredProperties = null)
		{
			foreach (var prop in typeof(T).GetProperties())
			{
				if (ignoredProperties != null && ignoredProperties.Contains(prop.Name)) continue;

				var expected = prop.GetValue(expectedObj);
				var actual = prop.GetValue(actualObj);
				Assert.Equal(expected, actual);
			}
		}
	}
}