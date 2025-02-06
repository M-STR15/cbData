namespace cbData.BE.BusinessLogic.Models.Products
{
	public class ProductBaseDto
	{
		public virtual string? Description { get; set; }

		public virtual int Id { get; set; }

		public virtual string Name { get; set; } = string.Empty;

	}
}
