using AutoMapper;
using cbData.BE.BusinessLogic.Models.Reports;
using cbData.BE.DB.Services;
using cbData.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;

namespace cbData.BE.BusinessLogic.Controllers
{
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	[SwaggerResponse(200, "Úspěšné získání položky/položek [Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/200)")]
	[SwaggerResponse(404, "Položka/Položky nenalezeny.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404)")]
	[SwaggerResponse(500, "Chyba serveru.[Další informace](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500)")]
	public class ReportController(ReportDbService reportDbService, IEventLogService eventLogService, IMapper mapper, IMemoryCache memoryCache) : ControllerBase
	{
		private readonly IEventLogService _eventLogService = eventLogService;
		private readonly ReportDbService _reportDbService = reportDbService;
		private readonly IMapper _mapper = mapper;
		private readonly IMemoryCache _memoryCache = memoryCache;

		private const int _cacheExpirationTimeMin = 5;

		[HttpGet("api/v1/reports/total-order-by-product")]
		public async Task<IActionResult> GetTotalOrdersByProductAsync()
		{
			try
			{
				const string cacheKey = "cachedData";
				if (!_memoryCache.TryGetValue(cacheKey, out List<TotalOrdersByProductDto> cachedData))
				{
					var model = await _reportDbService.GetTotalOrdersByProductAsync();
					if (model != null)
					{
						var modelDto = _mapper.Map<List<TotalOrdersByProductDto>>(model);
						_memoryCache.Set(cacheKey, modelDto, TimeSpan.FromMinutes(_cacheExpirationTimeMin));
						return Ok(modelDto);
					}
					else
					{
						return NotFound();
					}
				}

				return Ok(cachedData);
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("bae696e6-7847-405f-b067-5629021be68d"), ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}