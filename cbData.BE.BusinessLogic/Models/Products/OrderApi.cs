using System.ComponentModel.DataAnnotations;

namespace cbData.BE.DB.Models.Products
{
	public class OrderApi : IOrder
	{
		public OrderApi()
		{ }

		public OrderApi(Order order)
		{
			ProductId = order.ProductId;
			Quantity = order.Quantity;
		}

		public int Id { get; set; }

		public Product? Product { get; set; }

		[Key]
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public DateTime UpdateUtcDateTime { get; set; }
	}
}
