using AutoMapper;
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
	public class ProductController(ProductDbService productDbService, IEventLogService eventLogService, RequestBufferService requestBufferService, IMapper mapper) : ControllerBase
	{
		private readonly IMapper _mapper = mapper;
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
					var orderDto = _mapper.Map<OrderDto>(order);
					return order != null ? Ok(orderDto) : NotFound();
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
				//var model = orders?.Select(x => firstLevelOrder(x)).ToList();
				if (orders != null)
				{
					var ordersDto = _mapper.Map<List<OrderDto>>(orders);
					return Ok(ordersDto);
				}
				else
				{
					return NotFound();
				}
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
					var productDto = _mapper.Map<ProductDto>(product);
					return Ok(productDto);
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

		#endregion GET

		#region POST

		/// <summary>
		/// Přidá novou objednávku
		/// </summary>
		/// <param name="orderDto">Objednávka k přidání</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPost("api/v1/products/orders")]
		public async Task<IActionResult> AddOrderAsync([FromBody] OrderDto orderDto)
		{
			try
			{
				var order = _mapper.Map<Order>(orderDto);
				order = await _productDbService.AddOrderAsync(order);
				orderDto = _mapper.Map<OrderDto>(order);
				return orderDto == null ? BadRequest() : Ok(orderDto);
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
		/// <param name="orderDto">Objednávka k přidání</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPost("api/v1/products/orders-without-answer")]
		public async Task<IActionResult> AddOrderWithouttAnswerAsync([FromBody] OrderDto orderDto)
		{
			try
			{
				var order = _mapper.Map<Order>(orderDto);
				await _requestBufferServic.AddOrderAsync(order);
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
		/// <param name="productDto">Produkt k přidání</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPost("api/v1/products")]
		public async Task<IActionResult> AddProductAsync([FromBody] ProductDto productDto)
		{
			try
			{
				var product = _mapper.Map<Product>(productDto);
				product = await _productDbService.AddProductAsync(product);
				productDto = _mapper.Map<ProductDto>(product);
				return productDto == null ? BadRequest() : Ok(productDto);
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
		/// <param name="orderDto">Objednávka k aktualizaci</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPut("api/v1/products/orders")]
		public async Task<IActionResult> UpdateOrderAsync([FromBody] OrderDto orderDto)
		{
			try
			{
				var order = _mapper.Map<Order>(orderDto);
				order = await _productDbService.UpdateOrder(order);
				orderDto = _mapper.Map<OrderDto>(order);
				return orderDto == null ? BadRequest() : Ok(orderDto);
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
		/// <param name="productDto">Produkt k aktualizaci</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPut("api/v1/products/")]
		public async Task<IActionResult> UpdateProductAsync([FromBody] ProductDto productDto)
		{
			try
			{
				var product = _mapper.Map<Product>(productDto);
				product = await _productDbService.UpdateProduct(product);
				productDto = _mapper.Map<ProductDto>(product);
				return productDto == null ? BadRequest() : Ok(productDto);
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