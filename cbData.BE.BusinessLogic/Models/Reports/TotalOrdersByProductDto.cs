using cbData.BE.BusinessLogic.Models.Products;

namespace cbData.BE.BusinessLogic.Models.Reports
{
	public class TotalOrdersByProductDto
	{
		public ProductBaseDto? Product { get; set; }
		public int TotalOrders { get; set; }
	}
}