using cbData.BE.DB.Models.Products;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class ProductApi : IProduct
	{
		public ProductApi()
		{ }

		public ProductApi(string? description, int id, string name, ICollection<Order>? orders)
		{
			Description = description;
			Id = id;
			Name = name;
			Orders = orders;
		}

		public string? Description { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Order>? Orders { get; set; }
	}
}
