using cbData.BE.DB.Models.Products;
using Newtonsoft.Json;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class OrderDto : OrderBaseDto
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ProductBaseDto? Product { get; set; }

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

		private static ProductDto? convertProduct(Product? product)
		{
			if (product != null)
				return new ProductDto(product?.Id ?? 0, product?.Name ?? "", product?.Description, null);
			else
				return null;
		}
	}
}