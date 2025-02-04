using cbData.BE.DB.Models.Products;
using cbData.BE.DB.Services;
using System.Collections.Concurrent;

namespace cbData.BE.BusinessLogic.Services
{
	public class RequestBufferService:IDisposable
	{
		private ConcurrentQueue<Order> Orders { get; set; } = new();
		private readonly ProductDbService _productDbService;
		private readonly CancellationTokenSource _cts = new(); // Pro bezpečné zastavení vlákna
		public RequestBufferService(ProductDbService productDbService)
		{
			_productDbService = productDbService;
			Task.Run(() => processOrdersAsync(_cts.Token)); 
		}

		/// <summary>
		/// Přidá objednávku do fronty.
		/// </summary>
		/// <param name="order">Objednávka k přidání.</param>
		/// <returns>Vrací přidanou objednávku nebo null v případě chyby.</returns>
		public Task<Order?> AddOrderAsync(Order order)
		{
			try
			{
				Orders.Enqueue(order);
				return Task.FromResult<Order?>(order);
			}
			catch (Exception)
			{
				return Task.FromResult<Order?>(null);
			}
		}

		private async Task processOrdersAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested) 
			{
				while (Orders.TryDequeue(out var order))
				{
					try
					{
						await _productDbService.AddOrderAsync(order);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Chyba při zpracování objednávky: {ex.Message}");
					}
				}

				await Task.Delay(500); // Zabrání 100% vytížení CPU
			}
		}

		private void stopProcessing()
		{
			_cts.Cancel(); 
		}

		public void Dispose()
		{
			stopProcessing();
		}
	}
}
