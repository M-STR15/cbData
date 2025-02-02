using cbData.BE.DB.Models.Products;

namespace cbData.BE.BusinessLogic.Models.Reports
{
	public class TotalOrdersByProduct
	{
		public Product? Product { get; set; }

		public int TotalOrders { get; set; }

		public TotalOrdersByProduct()
		{ }

		public TotalOrdersByProduct(Product? product, int totalOrders)
		{
			Product = product;
			TotalOrders = totalOrders;
		}
	}
}