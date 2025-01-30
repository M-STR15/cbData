
namespace cbData.BE.DB.Models.Products
{
	public interface IProduct
	{
		string? Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		ICollection<Order>? Orders { get; set; }
	}
}