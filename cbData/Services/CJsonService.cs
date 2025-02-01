using cbData.Shared.Services;
using cbData.Shared.Stories;
using cbData.Stories;
using Newtonsoft.Json;

namespace cbData.Services
{
	public class CJsonService
	{
		private ProductStory? _productStory;
		private readonly PathsStory _pathsStory;
		private readonly IEventLogService _eventLogService;
		public CJsonService(ProductStory? productStory, PathsStory pathsStory, IEventLogService eventLogService)
		{
			_productStory = productStory;
			_pathsStory = pathsStory;
			_eventLogService = eventLogService;
		}
		public async Task SaveBufferDataToJsonAsync()
		{
			try
			{
				if (_productStory?.OrdersBuffer?.Orders != null)
				{
					var path = Path.Combine(_pathsStory.BaseDirectory, _pathsStory.FileDirectory);
					var fullPath = Path.Combine(path, _pathsStory.JsonBufferName);
					var data = _productStory.OrdersBuffer.Orders;
					var jsonString = JsonConvert.SerializeObject(data);

					if (jsonString != null)
					{
						if (!Directory.Exists(path))
							Directory.CreateDirectory(path);

						await File.WriteAllTextAsync(fullPath, jsonString);
					}
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("4dcd36dc-b947-4fba-b6e2-ddfeb4650df1"), ex.Message);
			}
		}

		public async Task<string?> ReadBufferDataAsync()
		{
			try
			{
				if (_productStory?.OrdersBuffer?.Orders != null)
				{
					var path = Path.Combine(_pathsStory.BaseDirectory, _pathsStory.FileDirectory);
					var fullPath = Path.Combine(path, _pathsStory.JsonBufferName);
					var data = _productStory.OrdersBuffer.Orders;

					if (File.Exists(fullPath))
					{
						var jsonText = await File.ReadAllTextAsync(fullPath);
						return jsonText;
					}
					else
					{
						return "";
					}
				}
				else
				{
					return "";
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("f70296d1-f9cc-4e43-86d7-158d9ea5779d"), ex.Message);
				return null;
			}
		}
	}
}
