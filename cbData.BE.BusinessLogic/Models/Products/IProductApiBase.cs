using cbData.BE.DB.Models.Products;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public interface IProductApiBase
	{
		string? Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		Product ToProduct();
	}
}