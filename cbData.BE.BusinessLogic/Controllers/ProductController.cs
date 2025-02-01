using cbData.BE.BusinessLogic.Models.Products;
using cbData.BE.DB.Models.Products;
using cbData.BE.DB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cbData.BE.BusinessLogic.Controllers
{
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	public class ProductController : ControllerBase
	{
		private ProductDbService _productDbService;
		public ProductController(ProductDbService productDbService)
		{
			_productDbService = productDbService;
		}
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
					return Ok(orderConvert);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)

			{
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
				return Ok(model);
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(ex.ToString());
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
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Převádí entitu Order na OrderApi
		/// </summary>
		/// <param name="order">Entita Order</param>
		/// <returns>Instance OrderApi</returns>
		private OrderApi firstLevelOrder(Order order)
		{
			return new OrderApi()
			{
				Id = order.Id,
				ProductId = order.ProductId,
				Quantity = order.Quantity,
				UpdateUtcDateTime = order.UpdateUtcDateTime,
				Product = new ProductApi
				{
					Id = order.Product.Id,
					Name = order.Product.Name,
					Description = order.Product.Description
				},
			};
		}

		/// <summary>
		/// Převádí entitu Product na ProductApi
		/// </summary>
		/// <param name="product">Entita Product</param>
		/// <returns>Instance ProductApi</returns>
		private ProductApi firstLevelProduct(Product product)
		{
			return new ProductApi()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Orders = product.Orders?.Select(x => new OrderApi(x)).ToList() ?? null
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
		public async Task<IActionResult> AddOrderAsync([FromBody] IOrderApiBase orderApi)
		{
			try
			{
				var order = await _productDbService.AddOrderAsync(orderApi.ToOrder());
				return Ok(order);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Přidá nový produkt
		/// </summary>
		/// <param name="productApi">Produkt k přidání</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPost("api/v1/products")]
		public async Task<IActionResult> AddProductAsync([FromBody] ProductApi productApi)
		{
			try
			{
				var product = await _productDbService.AddProductAsync(productApi.ToProduct());
				return Ok(product);
			}
			catch (Exception ex)
			{
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
		public async Task<IActionResult> UpdateOrderAsync([FromBody] IOrderApiBase orderApi)
		{
			try
			{
				var orders = await _productDbService.UpdateOrder(orderApi.ToOrder());
				return Ok(orders);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		/// <summary>
		/// Aktualizuje produkt
		/// </summary>
		/// <param name="productApi">Produkt k aktualizaci</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPut("api/v1/products/")]
		public async Task<IActionResult> UpdateProductAsync([FromBody] ProductApi productApi)
		{
			try
			{
				var product = await _productDbService.UpdateProduct(productApi.ToProduct());
				return Ok(product);
			}
			catch (Exception ex)
			{
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
				var orders = await _productDbService.DeleteOrderAsync(orderId);
				return NoContent();
			}
			catch (Exception ex)
			{
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
				var product = await _productDbService.DeleteProductAsync(productId);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion DELETE
	}
}
