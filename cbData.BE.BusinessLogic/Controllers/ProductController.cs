using cbData.BE.BusinessLogic.Models.Products;
using cbData.BE.BusinessLogic.Services;
using cbData.BE.DB.Models.Products;
using cbData.BE.DB.Services;
using cbData.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace cbData.BE.BusinessLogic.Controllers
{
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	[SwaggerResponse(200, "Úspěšné získání položky/položek [Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200)")]
	[SwaggerResponse(404, "Položka/Položky nenalezeny.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404)")]
	[SwaggerResponse(500, "Chyba serveru.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500)")]
	public class ProductController(ProductDbService productDbService, IEventLogService eventLogService, RequestBufferService requestBufferService) : ControllerBase
	{
		private readonly IEventLogService _eventLogService = eventLogService;
		private readonly ProductDbService _productDbService = productDbService;
		private readonly RequestBufferService _requestBufferServic = requestBufferService;
		#region GET

		/// <summary>
		/// Získá objednávku podle ID
		/// </summary>
		/// <param name="orderId">ID objednávky</param>
		/// <returns>HTTP odpověď</returns>
		[HttpGet("api/v1/products/orders/{orderId}")]
		public async Task<IActionResult> GetOrderAsync(int orderId)
		{
			try
			{
				var order = await _productDbService.GetOrderAsync(orderId);
				if (order != null)
				{
					var orderConvert = firstLevelOrder(order);
					return order != null ? Ok(orderConvert) : NotFound();
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("88512c81-4994-46c6-95c7-a3accbcafe20"), ex.Message);
				//Debug.WriteLine(ex.ToString());
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Získá všechny objednávky
		/// </summary>
		/// <returns>HTTP odpověď</returns>
		[HttpGet("api/v1/products/orders")]
		public async Task<IActionResult> GetOrdersAsync()
		{
			try
			{
				var orders = await _productDbService.GetOrdersAsync();
				var model = orders?.Select(x => firstLevelOrder(x)).ToList();
				return model != null ? Ok(model) : NotFound(); ;
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("533c3ac6-2298-4133-81fe-9d1ffe1f0626"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Získá produkt podle ID
		/// </summary>
		/// <param name="productId">ID produktu</param>
		/// <returns>HTTP odpověď</returns>
		[HttpGet("api/v1/products/{productId}")]
		public async Task<IActionResult> GetProductAsync(int productId)
		{
			try
			{
				var product = await _productDbService.GetProductAsync(productId);
				if (product != null)
				{
					var productConvert = firstLevelProduct(product);
					return Ok(productConvert);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("25206d29-7f6b-4b13-8753-cfd56b3f36b2"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Převádí entitu Order na OrderApi
		/// </summary>
		/// <param name="order">Entita Order</param>
		/// <returns>Instance OrderApi</returns>
		private static OrderDto firstLevelOrder(Order order)
		{
			return new OrderDto()
			{
				Id = order.Id,
				ProductId = order.ProductId,
				Quantity = order.Quantity,
				UpdateUtcDateTime = order.UpdateUtcDateTime,
				Product = new ProductDto
				{
					Id = order?.Product?.Id ?? 0,
					Name = order?.Product?.Name ?? "",
					Description = order?.Product?.Description
				},
			};
		}

		/// <summary>
		/// Převádí entitu Product na ProductApi
		/// </summary>
		/// <param name="product">Entita Product</param>
		/// <returns>Instance ProductApi</returns>
		private static ProductDto firstLevelProduct(Product product)
		{
			return new ProductDto()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Orders = product.Orders?.Select(x => new OrderDto(x)).ToList() ?? null
			};
		}

		#endregion GET

		#region POST

		/// <summary>
		/// Přidá novou objednávku
		/// </summary>
		/// <param name="orderApi">Objednávka k přidání</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPost("api/v1/products/orders")]
		public async Task<IActionResult> AddOrderAsync([FromBody] OrderDto orderApi)
		{
			try
			{
				var order = await _productDbService.AddOrderAsync(orderApi.ToOrder());
				return order == null ? BadRequest() : Ok(order);
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("1004f45c-0861-4dc0-8083-757fe0207019"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Přidá novou objednávku. U totoho API zpětného zaslání request objektu, jelikož toto api jenom přímá data a zpracováváje až později. 
		/// </summary>
		/// <param name="orderApi">Objednávka k přidání</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPost("api/v1/products/orders-without-answer")]
		public async Task<IActionResult> AddOrderWithouttAnswerAsync([FromBody] OrderDto orderApi)
		{
			try
			{
				await _requestBufferServic.AddOrderAsync(orderApi.ToOrder());
				return NoContent();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("cc9c768e-e7bf-4ced-9135-c6c0a98b42f6"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}


		/// <summary>
		/// Přidá nový produkt
		/// </summary>
		/// <param name="productApi">Produkt k přidání</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPost("api/v1/products")]
		public async Task<IActionResult> AddProductAsync([FromBody] ProductDto productApi)
		{
			try
			{
				var product = await _productDbService.AddProductAsync(productApi.ToProduct());
				return product == null ? BadRequest() : Ok(product);
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("6ba72af9-b92c-4781-a9e7-d6d2c2879e12"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		#endregion POST

		#region PUT

		/// <summary>
		/// Aktualizuje objednávku
		/// </summary>
		/// <param name="orderApi">Objednávka k aktualizaci</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPut("api/v1/products/orders")]
		public async Task<IActionResult> UpdateOrderAsync([FromBody] OrderDto orderApi)
		{
			try
			{
				var order = await _productDbService.UpdateOrder(orderApi.ToOrder());
				return order == null ? BadRequest() : Ok(order);
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("7fa30049-160d-4cbb-89c6-5b8cce92c1c4"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Aktualizuje produkt
		/// </summary>
		/// <param name="productApi">Produkt k aktualizaci</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPut("api/v1/products/")]
		public async Task<IActionResult> UpdateProductAsync([FromBody] ProductDto productApi)
		{
			try
			{
				var product = await _productDbService.UpdateProduct(productApi.ToProduct());
				return product == null ? BadRequest() : Ok(product);
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("5469acc6-1de1-4f84-9ca4-2833c2d5b1fa"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		#endregion PUT

		#region DELETE

		/// <summary>
		/// Smaže objednávku podle ID
		/// </summary>
		/// <param name="orderId">ID objednávky</param>
		/// <returns>HTTP odpověď</returns>
		[HttpDelete("api/v1/products/orders/{orderId}")]
		public async Task<IActionResult> DeleteOrderAsync(int orderId)
		{
			try
			{
				var result = await _productDbService.DeleteOrderAsync(orderId);
				return result ? NoContent() : BadRequest();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("04d698d3-220f-44b9-b3fc-1b8365fa6972"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Smaže produkt podle ID
		/// </summary>
		/// <param name="productId">ID produktu</param>
		/// <returns>HTTP odpověď</returns>
		[HttpDelete("api/v1/products/{productId}")]
		public async Task<IActionResult> DeleteProductAsync(int productId)
		{
			try
			{
				var result = await _productDbService.DeleteProductAsync(productId);
				return result ? NoContent() : BadRequest();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("dcbcfea4-3f96-4068-bf1f-ec7a824d1741"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		#endregion DELETE
	}
}