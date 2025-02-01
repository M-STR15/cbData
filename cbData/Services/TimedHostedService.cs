using cbData.BE.BusinessLogic.Models.Reports;
using cbData.BE.DB.Models.Products;
using cbData.Shared.Services;
using cbData.Shared.Stories;
using cbData.Stories;

namespace cbData.Services
{
	public class TimedHostedService : IHostedService, IDisposable
	{
		private Timer? _refreshBufferTimer;
		private ProductStory? _productStory;
		private readonly HttpClient _httpClient;
		private readonly IEventLogService _eventLogService;
		private readonly CJsonService _cJsonService;
		public TimedHostedService(IHttpClientFactory httpClientFactory, ProductStory? productStory, IEventLogService eventLogService, CJsonService cJsonService)
		{
			_productStory = productStory;
			_httpClient = httpClientFactory.CreateClient("ApiClient");
			_eventLogService = eventLogService;
			_cJsonService = cJsonService;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_refreshBufferTimer = new Timer(doWork, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(20));
			_eventLogService.WriteInformation(Guid.Parse("1f7650c4-65a8-4738-b8a2-13e5140f5cc1"), "Nastartování timeru pro ukládání hodnot do bufferu.");
			return Task.CompletedTask;
		}

		private async void doWork(object state)
		{
			try
			{
				await refreshBufferData();
				await _cJsonService.SaveBufferDataToJsonAsync();
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("37fdf305-03b4-46dd-961e-2af6e7c9b013"), ex.Message);
			}
		}
		private async Task refreshBufferData()
		{
			try
			{
				if (_httpClient != null)
				{
					var result = await _httpClient.GetAsync("/api/v1/reports/total-order-by-product");

					if (result.IsSuccessStatusCode)
					{
						var resultData = await result.Content.ReadFromJsonAsync<List<TotalOrdersByProductApi>>();
						if (_productStory != null && resultData != null && _productStory.OrdersBuffer != null)
						{
							_productStory.OrdersBuffer.LastUpdate = DateTime.UtcNow;
							_productStory.OrdersBuffer.TotalOrdersByProduct = resultData;
						}
					}
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("38eeba7d-e0f4-4fc2-a698-07db6b2f0569"), ex.Message);
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_refreshBufferTimer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_refreshBufferTimer?.Dispose();
		}
	}
}
