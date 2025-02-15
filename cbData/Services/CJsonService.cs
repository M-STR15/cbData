﻿using cbData.Shared.Services;
using cbData.Shared.Stories;
using cbData.Stories;
using Newtonsoft.Json;

namespace cbData.Services
{
	public class CJsonService(ProductStory? productStory, PathsStory pathsStory, IEventLogService eventLogService)
	{
		private readonly IEventLogService _eventLogService = eventLogService;
		private readonly PathsStory _pathsStory = pathsStory;
		private readonly ProductStory? _productStory = productStory;
		public async Task<string?> ReadBufferDataAsync()
		{
			try
			{
				if (_productStory?.OrdersBuffer?.TotalOrdersByProduct != null)
				{
					var path = Path.Combine(_pathsStory.BaseDirectory, _pathsStory.FileDirectory);
					var fullPath = Path.Combine(path, _pathsStory.JsonBufferName);
					var data = _productStory.OrdersBuffer.TotalOrdersByProduct;

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

		public async Task SaveBufferDataToJsonAsync()
		{
			try
			{
				if (_productStory?.OrdersBuffer?.TotalOrdersByProduct != null)
				{
					var path = Path.Combine(_pathsStory.BaseDirectory, _pathsStory.FileDirectory);
					var fullPath = Path.Combine(path, _pathsStory.JsonBufferName);
					var data = _productStory.OrdersBuffer.TotalOrdersByProduct;
					var jsonString = JsonConvert.SerializeObject(data, new JsonSerializerSettings
					{
						NullValueHandling = NullValueHandling.Ignore
					});

					if (jsonString != null)
					{
						if (!Directory.Exists(path))
							_ = Directory.CreateDirectory(path);

						await File.WriteAllTextAsync(fullPath, jsonString);
					}
				}
			}
			catch (Exception ex)
			{
				_eventLogService.WriteError(Guid.Parse("4dcd36dc-b947-4fba-b6e2-ddfeb4650df1"), ex.Message);
			}
		}
	}
}