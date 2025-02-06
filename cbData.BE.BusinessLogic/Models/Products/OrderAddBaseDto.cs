namespace cbData.BE.BusinessLogic.Models.Products
{
	public class OrderAddBaseDto
	{
		public virtual int ProductId { get; set; }
		public virtual int Quantity { get; set; }

		public OrderAddBaseDto()
		{ }
	}
}