using cbData.BE.DB.Models.Products;

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
		public List<Order> Orders { get; set; } = new();
	}
}
