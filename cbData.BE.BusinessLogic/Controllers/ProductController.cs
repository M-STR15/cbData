using cbData.BE.DB.DataContext;
using cbData.BE.DB.Models.Products;
using cbData.BE.DB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cbData.BE.BusinessLogic.Controllers
{
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	public class ProductController : ControllerBase
	{
		private readonly ProductDbService _productDbService;
		public ProductController(IDbContextFactory<CbDataDbContext> contextFactory, ProductDbService productDbService)
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
				var orders = await _productDbService.GetOrderAsync(orderId);
				return Ok(orders);
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
				var model = orders?.Select(x => new OrderApi(x));
				return Ok(model);
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(ex.ToString());
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion GET
		#region POST
		/// <summary>
		/// Přidá novou objednávku
		/// </summary>
		/// <param name="orderApi">Objednávka k přidání</param>
		/// <returns>HTTP odpověď</returns>
		[HttpPost("api/v1/products/orders")]
		public async Task<IActionResult> AddOrderAsync([FromBody] OrderApi orderApi)
		{
			try
			{
				var order = await _productDbService.AddOrderAsync(orderApi.ToOrder());
				return CreatedAtAction("", order);
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
		public async Task<IActionResult> UpdateOrderAsync([FromBody] OrderApi orderApi)
		{
			try
			{
				var orders = await _productDbService.UpdateOrder(orderApi.ToOrder());
				return CreatedAtAction("", orders);
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
		#endregion DELETE

	}
}
