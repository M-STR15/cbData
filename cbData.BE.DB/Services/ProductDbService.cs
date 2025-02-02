#pragma warning disable IDE0058
using cbData.BE.DB.DataContext;
using cbData.BE.DB.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cbData.BE.DB.Services
{
	public class ProductDbService(IDbContextFactory<CbDataDbContext> contextFactory)
	{
		private readonly IDbContextFactory<CbDataDbContext> _contextFactory = contextFactory;

		public async Task<Order?> AddOrderAsync(Order order)
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
			catch (Exception)
			{
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
			catch (Exception)
			{
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
					var existingOrder = await db.Orders.Where(o => o.Id == order.Id).FirstOrDefaultAsync();

					if (existingOrder == null)
					{
						throw new Exception("Objednávka nenalezena.");
					}

					existingOrder.ProductId = order.ProductId;
					existingOrder.Quantity = order.Quantity;
					existingOrder.UpdateUtcDateTime = DateTime.UtcNow;

					db.Orders.Update(existingOrder);
					await db.SaveChangesAsync();

					return existingOrder;
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

					var existingProduct = await db.Products.Where(o => o.Id == product.Id).FirstOrDefaultAsync();

					if (existingProduct == null)
					{
						throw new Exception("Produkt nenalezeno.");
					}

					existingProduct.Name = product.Name;
					existingProduct.Description = product.Description;

					db.Products.Update(existingProduct);
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