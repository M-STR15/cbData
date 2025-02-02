using cbData.BE.DB.Models.Products;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public interface IOrderApiBase
	{
		int Id { get; set; }
		int ProductId { get; set; }
		int Quantity { get; set; }
		DateTime UpdateUtcDateTime { get; set; }

		Order ToOrder();
	}
}