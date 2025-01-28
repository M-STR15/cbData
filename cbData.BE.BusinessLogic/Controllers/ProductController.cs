using cbData.BE.DB.DataContext;
using cbData.BE.DB.Models.Products;
using cbData.BE.DB.Services;
using Microsoft.AspNetCore.Authorization;
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
		[HttpGet("api/v1.0/Procuts/GET/Orders/All")]
		[AllowAnonymous]
		public IActionResult GetOrders()
		{
			try
			{
				var orders = _productDbService.GetOrders();
				var model = orders?.Select(x => new OrderApi(x));
				return model == null ? NotFound() : Ok(model);
			}
			catch (Exception ex)
			{
				//Debug.WriteLine(ex.ToString());
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
		#endregion GET

	}
}
