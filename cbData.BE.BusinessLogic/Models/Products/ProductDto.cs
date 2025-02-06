using cbData.BE.DB.Models.Products;
using Newtonsoft.Json;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class ProductDto : IProductBase
	{
		public string? Description { get; set; }

		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ICollection<OrderDto>? Orders { get; set; }

		public ProductDto()
		{ }

		public ProductDto(int id, string name, string? description, ICollection<OrderDto>? orders = null)
		{
			Description = description;
			Id = id;
			Name = name;
			Orders = orders;
		}

		public ProductDto(Product? product) : this(product?.Id ?? 0, product?.Name ?? "", product?.Description)
		{
			Orders = product?.Orders?.Select(x => new OrderDto(x))?.ToList();
		}
		public Product ToProduct()
		{
			return new Product(Id, Name, Description);
		}
	}
}