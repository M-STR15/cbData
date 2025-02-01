using cbData.BE.BusinessLogic.Models.Products;

namespace cbData.BE.BusinessLogic.Models.Reports
{
	public class TotalOrdersByProductApi
	{
		public ProductApi? Product { get; set; }
		public int TotalOrders { get; set; }

		public TotalOrdersByProductApi()
		{ }

		public TotalOrdersByProductApi(ProductApi product, int totalOrders)
		{
			Product = product;
			TotalOrders = totalOrders;
		}
	}
}
