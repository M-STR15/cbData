using cbData.BE.DB.Models.Products;
using cbData.Stories;
using System.Net.Http;

namespace cbData.Services
{
	public class TimedHostedService : IHostedService, IDisposable
	{
		private Timer? _timer;
		private ProductStory? _productStory;
		private readonly HttpClient _httpClient;
		public TimedHostedService(IHttpClientFactory httpClientFactory, ProductStory? productStory)
		{
			_productStory = productStory;
			_httpClient = httpClientFactory.CreateClient("ApiClient");
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_timer = new Timer(doWork, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(20));

			return Task.CompletedTask;
		}

		private async void doWork(object state)
		{
			if (_httpClient != null)
			{
				var result = await _httpClient.GetAsync("/api/v1/products/orders");

				if (result.IsSuccessStatusCode)
				{
					var resultData = await result.Content.ReadFromJsonAsync<List<OrderApi>>();
					if (_productStory != null && resultData != null)
					{
							_productStory.OrdersBuffer.LastUpdate = DateTime.UtcNow;
						_productStory.OrdersBuffer.Orders = resultData;
					}
				}

			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
