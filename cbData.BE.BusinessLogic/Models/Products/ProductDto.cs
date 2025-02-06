using cbData.BE.DB.Models.Products;
using Newtonsoft.Json;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class ProductDto : ProductBaseDto, IProductBase
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ICollection<OrderBaseDto>? Orders { get; set; }
	}
}