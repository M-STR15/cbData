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
		[HttpGet("api/v1/products/orders")]
		public IActionResult GetOrders()
		{
			try
			{
				var orders = _productDbService.GetOrders();
				var model = orders?.Select(x => new OrderApi(x));
				return Ok(model);
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(ex.ToString());
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		[HttpGet("api/v1/products/orders/{orderId}")]
		public IActionResult GetOrder(int orderId)
		{
			try
			{
				var orders = _productDbService.GetOrder(orderId);
				return Ok(orders);
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(ex.ToString());
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion GET

		#region POST
		[HttpPost("api/v1/products/orders")]
		public IActionResult AddOrder([FromBody] OrderApi orderApi)
		{
			try
			{
				var order = _productDbService.AddOrder(orderApi.ToOrder());
				return CreatedAtAction("", order);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion POST

		#region PUT
		[HttpPut("api/v1/products/orders")]
		public IActionResult UpdateOrder([FromBody] OrderApi orderApi)
		{
			try
			{
				var orders = _productDbService.UpdateOrder(orderApi.ToOrder());
				return CreatedAtAction("", orders);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion PUT

		#region DELETE
		[HttpDelete("api/v1/products/orders/{orderId}")]
		public IActionResult DeleteOrder(int orderId)
		{
			try
			{
				var orders = _productDbService.DeleteOrder(orderId);
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
