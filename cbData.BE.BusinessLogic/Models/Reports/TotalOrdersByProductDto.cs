using cbData.BE.BusinessLogic.Models.Products;

namespace cbData.BE.BusinessLogic.Models.Reports
{
	public class TotalOrdersByProductDto
	{
		public ProductDto? Product { get; set; }
		public int TotalOrders { get; set; }

		public TotalOrdersByProductDto()
		{ }

		public TotalOrdersByProductDto(ProductDto product, int totalOrders)
		{
			Product = product;
			TotalOrders = totalOrders;
		}
	}
}