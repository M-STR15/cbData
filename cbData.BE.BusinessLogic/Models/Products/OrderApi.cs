using System.ComponentModel.DataAnnotations;

namespace cbData.BE.DB.Models.Products
{
	public class OrderApi : IOrder
	{
		public OrderApi()
		{ }

		public OrderApi(Order order)
		{
			Id = order.Id;
			ProductId = order.ProductId;
			Quantity = order.Quantity;
			UpdateUtcDateTime = order.UpdateUtcDateTime;
			Product = order.Product;
		}

		public int Id { get; set; }

		public Product? Product { get; set; }

		[Key]
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public DateTime UpdateUtcDateTime { get; set; }

		public Order ToOrder()
		{
			return new Order(ProductId, Quantity);
		}
	}
}
