namespace cbData.BE.DB.Models.Products
{
	public interface IOrder
	{
		int Id { get; set; }
		Product? Product { get; set; }
		int ProductId { get; set; }
		int Quantity { get; set; }
	}
}