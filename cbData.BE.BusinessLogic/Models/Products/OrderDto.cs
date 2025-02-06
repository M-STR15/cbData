using Newtonsoft.Json;

namespace cbData.BE.BusinessLogic.Models.Products
{
	public class OrderDto : OrderBaseDto
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public ProductBaseDto? Product { get; set; }
		public DateTime UpdateUtcDateTime { get; set; }
		public OrderDto()
		{ }
	}
}