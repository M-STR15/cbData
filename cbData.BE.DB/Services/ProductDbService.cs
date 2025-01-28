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
	}
}
