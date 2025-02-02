using cbData.BE.BusinessLogic.Models.Reports;

namespace cbData.Models
{
	public class OrdersBuffer
	{
		/// <summary>
		/// Poslední aktualizace dat.
		/// </summary>
		public DateTime LastUpdate { get; set; }

		/// <summary>
		/// Hodnoty uložné v paměti od poslední aktualizace.
		/// </summary>
		public List<TotalOrdersByProductApi> TotalOrdersByProduct { get; set; } = new();
	}
}