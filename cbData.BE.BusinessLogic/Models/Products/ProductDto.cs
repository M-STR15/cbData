using cbData.BE.DB.Models.Products;
using Newtonsoft.Json;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class ProductDto : ProductBaseDto, IProductBase
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ICollection<OrderBaseDto>? Orders { get; set; }

		public ProductDto()
		{ }

		public ProductDto(int id, string name, string? description, ICollection<OrderBaseDto>? orders = null)
		{
			Description = description;
			Id = id;
			Name = name;
			Orders = orders;
		}

		public ProductDto(Product? product) : this(product?.Id ?? 0, product?.Name ?? "", product?.Description)
		{
			Orders = product?.Orders?.Select(x => new OrderBaseDto(x))?.ToList();
		}
	}
}