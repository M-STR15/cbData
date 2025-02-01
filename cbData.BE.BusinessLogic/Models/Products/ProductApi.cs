using cbData.BE.DB.Models.Products;
using System.Text.Json.Serialization;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class ProductApi : IProduct, IProductApiBase, IProductApiAddBase
	{
		public ProductApi()
		{ }

		public ProductApi(int id, string name, string? description, ICollection<OrderApi>? orders)
		{
			Description = description;
			Id = id;
			Name = name;
			Orders = orders;
		}

		public string? Description { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public ICollection<OrderApi>? Orders { get; set; }

		public Product ToProduct()
		{
			return new Product(Id, Name, Description);
		}
	}
}
