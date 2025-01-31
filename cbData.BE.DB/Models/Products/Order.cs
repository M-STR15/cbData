using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cbData.BE.DB.Models.Products
{
	[Table("Orders", Schema = "Products")]
	public class Order : Stamp, IOrder
	{
		public Order()
		{
			UpdateUtcDateTime = DateTime.UtcNow;
		}

		public Order(int productId, int quantity) : this()
		{
			ProductId = productId;
			Quantity = quantity;
		}

		public Order(int id, int productId, int quantity, DateTime? updateDateTime = null) : this(productId, quantity)
		{
			Id = id;

			if (updateDateTime != null)
			{
				UpdateUtcDateTime = (DateTime)updateDateTime;
			}
		}

		[Key]
		public int Id { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product? Product { get; set; }

		[Required]
		public int ProductId { get; set; }
		[Required]
		public int Quantity { get; set; }
	}
}
