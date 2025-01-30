namespace cbData.Services
{
	public class TimedHostedService : IHostedService, IDisposable
	{
		private Timer _timer;
		private readonly ILogger<TimedHostedService> _logger;

		public TimedHostedService(ILogger<TimedHostedService> logger)
		{
			_logger = logger;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Timed Hosted Service is starting.");

			// Spustí timer, který se spustí po 5 sekundách a následně každých 20 sekund
			_timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(20));

			return Task.CompletedTask;
		}

		private void DoWork(object state)
		{
			_logger.LogInformation("Timer triggered at: {time}", DateTimeOffset.Now);
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
