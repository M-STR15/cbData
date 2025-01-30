using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cbData.BE.DB.Models.Products
{
	[Table("Products", Schema = "Products")]
	[Index(nameof(Name), IsUnique = true)]
	public class Product : IProduct
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }

		public ICollection<Order>? Orders { get; set; }

		public Product()
		{ }
		public Product(int id, string name, string? description = "")
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
