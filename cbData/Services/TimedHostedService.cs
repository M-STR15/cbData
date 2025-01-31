using cbData.BE.DB.Models.Products;
using cbData.Stories;

namespace cbData.Services
{
	public class TimedHostedService : IHostedService, IDisposable
	{
		private Timer _timer;
		private readonly ILogger<TimedHostedService> _logger;

		private ProductStory _productStory { get; set; }
		private CbDataHttpClientService _clientService { get; set; }
		public TimedHostedService(ILogger<TimedHostedService> logger, CbDataHttpClientService clientService, ProductStory productStory)
		{
			_logger = logger;
			_clientService = clientService;
			_productStory = productStory;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Timed Hosted Service is starting.");
			// Spustí timer, který se spustí po 5 sekundách a následně každých 20 sekund
			_timer = new Timer(doWork, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(20));

			return Task.CompletedTask;
		}

		private async void doWork(object state)
		{
			_logger.LogInformation("Timer triggered at: {time}", DateTimeOffset.Now);
			if (_clientService != null)
			{
				var orders = await _clientService.GetAsync<List<Order?>>("/api/v1/products/orders");
				if (orders != null && _productStory != null)
				{
					_productStory.OrdersBuffer.LastUpdate = DateTime.UtcNow;
					_productStory.OrdersBuffer.Orders = orders;
				}
			}
			// Tady můžeš spustit svůj kód, který chceš vykonávat periodicky
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Timed Hosted Service is stopping.");

			// Zastaví timer, pokud aplikace běží
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
