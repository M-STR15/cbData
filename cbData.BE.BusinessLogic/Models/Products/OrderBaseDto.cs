using cbData.BE.DB.Models.Products;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class OrderBaseDto
	{
		public virtual int Id { get; set; }
		public virtual int ProductId { get; set; }
		public virtual int Quantity { get; set; }
		public DateTime UpdateUtcDateTime { get; set; }

		public OrderBaseDto()
		{ }

		public OrderBaseDto(Order order)
		{
			Id = order.Id;
			ProductId = order.ProductId;
			Quantity = order.Quantity;
			UpdateUtcDateTime = order.UpdateUtcDateTime;
		}
	}
}