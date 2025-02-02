using cbData.BE.BusinessLogic.Models.Products;
using cbData.BE.BusinessLogic.Models.Reports;
using cbData.BE.DB.Services;
using cbData.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cbData.BE.BusinessLogic.Controllers
{
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
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
					var modelConvert = model.Select(x => new TotalOrdersByProductApi(new ProductApi(x?.Product), x?.TotalOrders ?? 0)).ToList();
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
