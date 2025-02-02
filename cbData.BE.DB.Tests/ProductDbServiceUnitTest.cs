#pragma warning disable IDE0058

using cbData.BE.BusinessLogic.Models.Products;
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
							.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unikátní DB pro každý test
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

		[Fact]
		public async Task AddProductAsync_ReturnsOkResult_WhenProductIsAdded()
		{
			await using var context = await createContextAsync(); // Ensure each test gets a fresh context

			var productApi = new ProductApi { Name = "Test Product", Description = "Test Description" };
			var product = new Product { Name = "Test Product", Description = "Test Description" };

			// Explicitly delete and recreate the database before each test
			await context.Database.EnsureDeletedAsync();
			await context.Database.EnsureCreatedAsync();

			var result = await _service.AddProductAsync(product);
			var test = await _service.GetProductAsync(product.Id);
			test = await _service.GetProductAsync(product.Id);
			test = await _service.GetProductAsync(product.Id);

			Assert.NotNull(result);
			Assert.Equal(product.Id, result.Id);
			Assert.Equal(test.Id, result.Id);
		}
	}
}