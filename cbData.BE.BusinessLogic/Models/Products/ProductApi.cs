using cbData.BE.DB.Models.Products;
using Newtonsoft.Json;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class ProductApi : IProduct, IProductApiBase, IProductApiAddBase
	{
		public ProductApi()
		{ }

		public ProductApi(int id, string name, string? description, ICollection<OrderApi>? orders = null)
		{
			Description = description;
			Id = id;
			Name = name;
			Orders = orders;
		}

		public ProductApi(Product? product) : this(product?.Id ?? 0, product?.Name ?? "", product?.Description)
		{
			Orders = product?.Orders?.Select(x => new OrderApi(x))?.ToList();
		}

		public string? Description { get; set; }
		public int Id { get; set; }
<<<<<<< HEAD
		public string Name { get; set; } = string.Empty;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ICollection<OrderApi>? Orders { get; set; }

		public Product ToProduct()
		{
			return new Product(Id, Name, Description);
		}
	}
}