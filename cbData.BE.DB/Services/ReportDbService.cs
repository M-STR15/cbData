using cbData.BE.BusinessLogic.Models.Reports;
using cbData.BE.DB.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cbData.BE.DB.Services
{
	public class ReportDbService
	{
		private readonly IDbContextFactory<CbDataDbContext> _contextFactory;

		public ReportDbService(IDbContextFactory<CbDataDbContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<List<TotalOrdersByProduct>?> GetTotalOrdersByProductAsync()
		{
			try
			{
				var db = await _contextFactory.CreateDbContextAsync();

				db.ChangeTracker.Clear();

				var result = await db.Orders
				.GroupBy(o => o.Product)
				.Select(g => new { Product = g.Key, TotalOrders = g.Sum(o => o.Quantity) })
				.ToListAsync();

				return result.Select(x => new TotalOrdersByProduct(x.Product, x.TotalOrders)).ToList();
			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return null;
			}
		}
	}
}