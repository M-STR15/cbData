using cbData.BE.DB.Models.Products;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class OrderDto 
	{
		public int Id { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ProductDto? Product { get; set; }

		[Key]
		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public DateTime UpdateUtcDateTime { get; set; }

		public OrderDto()
		{ }

		public OrderDto(Order order)
		{
			Id = order.Id;
			ProductId = order.ProductId;
			Quantity = order.Quantity;
			UpdateUtcDateTime = order.UpdateUtcDateTime;
			Product = convertProduct(order.Product);
		}
		public Order ToOrder()
		{
			return new Order(Id, ProductId, Quantity, UpdateUtcDateTime);
		}

		private static ProductDto? convertProduct(Product? product)
		{
			if (product != null)
				return new ProductDto(product?.Id ?? 0, product?.Name ?? "", product?.Description, null);
			else
				return null;
		}
	}
}