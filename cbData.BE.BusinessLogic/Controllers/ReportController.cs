using cbData.BE.BusinessLogic.Models.Products;
using cbData.BE.BusinessLogic.Models.Reports;
using cbData.BE.DB.Services;
using cbData.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace cbData.BE.BusinessLogic.Controllers
{
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	[SwaggerResponse(200, "Úspěšné získání položky/položek [Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200)")]
	[SwaggerResponse(404, "Položka/Položky nenalezeny.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404)")]
	[SwaggerResponse(500, "Chyba serveru.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500)")]
	public class ReportController : ControllerBase
	{
		private ReportDbService _reportDbService;
		private IEventLogService _eventLogService;

		public ReportController(ReportDbService reportDbService, IEventLogService eventLogService)
		{
			_reportDbService = reportDbService;
			_eventLogService = eventLogService;
		}

		[HttpGet("api/v1/reports/total-order-by-product")]
		public async Task<IActionResult> GetTotalOrdersByProductAsync()
		{
			try
			{
				var model = await _reportDbService.GetTotalOrdersByProductAsync();
				if (model != null)
				{
					//var json= JsonConvert.SerializeObject(model);
					var modelConvert = model.Select(x => new TotalOrdersByProductApi(new ProductApi(x.Product), x.TotalOrders)).ToList();
					return Ok(model);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("bae696e6-7847-405f-b067-5629021be68d"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}