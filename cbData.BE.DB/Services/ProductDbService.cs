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

		public async Task<List<Order>?> GetOrdersAsync()
		{
			try
			{
				using (var db = await _contextFactory.CreateDbContextAsync())
				{
					db.ChangeTracker.Clear();
					var list = await db.Orders.ToListAsync();
					return list;
				}

			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}

		public async Task<Order?> GetOrderAsync(int orderId)
		{
			try
			{
				using (var db = await _contextFactory.CreateDbContextAsync())
				{
					db.ChangeTracker.Clear();
					var order = await db.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
					return order;
				}

			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}

		public async Task<Order?> AddOrderAsync(Order order)
		{
			try
			{
				using (var db = await _contextFactory.CreateDbContextAsync())
				{
					db.ChangeTracker.Clear();
					await db.AddAsync(order);
					await db.SaveChangesAsync();

					return order;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<Order?> UpdateOrder(Order order)
		{
			try
			{
				using (var db = await _contextFactory.CreateDbContextAsync())
				{
					db.ChangeTracker.Clear();
					db.Update(order);
					await db.SaveChangesAsync();

					return order;
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
				using (var db = await _contextFactory.CreateDbContextAsync())
				{
					var order = db.Orders.FirstOrDefault(x => x.Id == orderId);
					if (order != null)
					{
						db.Remove(order);
						await db.SaveChangesAsync();
					}

					return true;
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
				using (var db = await _contextFactory.CreateDbContextAsync())
				{
					return await db.Orders.AnyAsync(x => x.Id == orderId);
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
