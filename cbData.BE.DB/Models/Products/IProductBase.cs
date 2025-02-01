namespace cbData.BE.DB.Models.Products
{
	public interface IProductBase
	{
		string? Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
	}
}