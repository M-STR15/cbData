using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cbData.BE.DB.Models.Products
{
	[Table("Orders", Schema = "Products")]
	public class Order
	{
		[Key]
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
