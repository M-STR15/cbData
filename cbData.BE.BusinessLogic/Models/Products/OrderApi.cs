using cbData.BE.BusinessLogic.Models.Products;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace cbData.BE.DB.Models.Products
{
	public class OrderApi : IOrderApiBase
	{
		public int Id { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ProductApi? Product { get; set; }

		[Key]
		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public DateTime UpdateUtcDateTime { get; set; }

		public OrderApi()
		{ }

		public OrderApi(Order order)
		{
			Id = order.Id;
			ProductId = order.ProductId;
			Quantity = order.Quantity;
			UpdateUtcDateTime = order.UpdateUtcDateTime;
			Product = convertProduct(order.Product);
		}
		public Order ToOrder()
		{
			return new Order(ProductId, Quantity);
		}

		private ProductApi? convertProduct(Product? product)
		{
			if (product != null)
				return new ProductApi(product?.Id ?? 0, product?.Name ?? "", product?.Description, null);
			else
				return null;
		}
	}
}