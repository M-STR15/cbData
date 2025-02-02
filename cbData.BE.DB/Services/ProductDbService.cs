using cbData.BE.DB.DataContext;
using cbData.BE.DB.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cbData.BE.DB.Services
{
	public class ProductDbService 
	{
		private readonly IDbContextFactory<CbDataDbContext> _contextFactory;

		public ProductDbService(IDbContextFactory<CbDataDbContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<IOrder?> AddOrderAsync(IOrder order)
		{
			try
			{
				if (_contextFactory != null)
				{
					var db = await _contextFactory.CreateDbContextAsync();
					db.ChangeTracker.Clear();
					await db.AddAsync(order);
					await db.SaveChangesAsync();

					return order;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				var test = ex.ToString();
				return null;
			}
		}

		public async Task<Product?> AddProductAsync(Product product)
		{
			try
			{
				if (_contextFactory != null)
				{
					var db = await _contextFactory.CreateDbContextAsync();
					db.ChangeTracker.Clear();
					await db.AddAsync(product);
					await db.SaveChangesAsync();

					return product;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				var test = ex;
				return null;
			}
		}

		public async Task<bool> DeleteOrderAsync(int orderId)
		{
			try
			{
				if (_contextFactory != null)
				{
					var db = await _contextFactory.CreateDbContextAsync();
					var entity = await db.Set<Order>().FindAsync(orderId);
					if (entity != null)
					{
						db.Remove(entity);
						await db.SaveChangesAsync();
					}

					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> DeleteProductAsync(int orderId)
		{
			try
			{
				if (_contextFactory != null)
				{
					var db = await _contextFactory.CreateDbContextAsync();
					var entity = await db.Set<Product>().FindAsync(orderId);
					if (entity != null)
					{
						db.Remove(entity);
						await db.SaveChangesAsync();
					}

					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> ExistOrderAsync(int orderId)
		{
			try
			{
				var db = await _contextFactory.CreateDbContextAsync();
				return await db.Orders.AnyAsync(x => x.Id == orderId);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<Order?> GetOrderAsync(int orderId)
		{
			try
			{
				var db = await _contextFactory.CreateDbContextAsync();
				db.ChangeTracker.Clear();
				var order = await db.Orders.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == orderId);
				return order;
			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}

		public async Task<List<Order>?> GetOrdersAsync()
		{
			try
			{
				var db = await _contextFactory.CreateDbContextAsync();
				db.ChangeTracker.Clear();
				var list = await db.Orders.Include(x => x.Product).ToListAsync();
				return list;
			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}

		public async Task<Product?> GetProductAsync(int productId)
		{
			try
			{
				var db = await _contextFactory.CreateDbContextAsync();
				db.ChangeTracker.Clear();
				var product = await db.Products.Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == productId);
				return product;
			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}

		public async Task<List<Product>?> GetProductsAsync()
		{
			try
			{
				var db = await _contextFactory.CreateDbContextAsync();
				db.ChangeTracker.Clear();
				var list = await db.Products.Include(x => x.Orders).ToListAsync();
				return list;
			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}

		public async Task<Order?> UpdateOrder(Order order)
		{
			try
			{
				if (_contextFactory != null)
				{
					var db = await _contextFactory.CreateDbContextAsync();
					db.ChangeTracker.Clear();
					db.Update(order);
					await db.SaveChangesAsync();

					return order;
				}
				else
				{
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<Product?> UpdateProduct(Product product)
		{
			try
			{
				if (_contextFactory != null)
				{
					var db = await _contextFactory.CreateDbContextAsync();
					db.ChangeTracker.Clear();
					db.Update(product);
					await db.SaveChangesAsync();

					return product;
				}
				else
				{
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}