using cbData.BE.DB.DataContext;
using cbData.BE.DB.Models.Reports;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cbData.BE.DB.Services
{
	public class ReportDbService(IDbContextFactory<CbDataDbContext> contextFactory)
	{
		private readonly IDbContextFactory<CbDataDbContext> _contextFactory = contextFactory;

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