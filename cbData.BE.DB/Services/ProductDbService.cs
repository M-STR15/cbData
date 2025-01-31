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

		public List<Order>? GetOrders()
		{
			try
			{
				using (var db = _contextFactory.CreateDbContext())
				{
					db.ChangeTracker.Clear();
					var list = db.Orders.ToList();
					return list;
				}

			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}

		public Order? GetOrder(int orderId)
		{
			try
			{
				using (var db = _contextFactory.CreateDbContext())
				{
					db.ChangeTracker.Clear();
					var order = db.Orders.FirstOrDefault(x => x.Id == orderId);
					return order;
				}

			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}

		public Order? AddOrder(Order order)
		{
			try
			{
				using (var db = _contextFactory.CreateDbContext())
				{
					db.ChangeTracker.Clear();
					db.Add(order);
					db.SaveChanges();

					return order;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		public Order? UpdateOrder(Order order)
		{
			try
			{
				using (var db = _contextFactory.CreateDbContext())
				{
					db.ChangeTracker.Clear();
					db.Update(order);
					db.SaveChanges();

					return order;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		public bool DeleteOrder(int orderId)
		{
			try
			{
				using (var db = _contextFactory.CreateDbContext())
				{
					var order = db.Orders.FirstOrDefault(x => x.Id == orderId);
					if (order != null)
					{
						db.Remove(order);
						db.SaveChanges();
					}

					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool ExistOrder(int orderId)
		{
			try
			{
				using (var db = _contextFactory.CreateDbContext())
				{
					return db.Orders.Any(x => x.Id == orderId);
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
