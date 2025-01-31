using cbData.BE.DB.Models.Products;
using System.Text.Json;

namespace cbData.Services
{
	public class CbDataHttpClientService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CbDataHttpClientService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
		{
			_httpClientFactory = httpClientFactory;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<T?> GetAsync<T>(string endpoint)
		{
			var client = _httpClientFactory.CreateClient();
			var request = _httpContextAccessor.HttpContext?.Request;

			if (request != null)
			{
				var baseAddress = $"{request.Scheme}://{request.Host.Value}/";
				if (Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
				{
					client.BaseAddress = uri;
				}
				else
				{
					throw new InvalidOperationException("Neplatný formát URI pro BaseAddress.");
				}
			}
			else
			{
				throw new InvalidOperationException("HttpContext není dostupný.");
			}

			var response = await client.GetAsync(endpoint);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<T>();
		}

		public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T content)
		{
			var client = _httpClientFactory.CreateClient();
			var request = _httpContextAccessor.HttpContext?.Request;

			if (request != null)
			{
				var baseAddress = $"{request.Scheme}://{request.Host.Value}/";
				if (Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
				{
					client.BaseAddress = uri;
				}
				else
				{
					throw new InvalidOperationException("Neplatný formát URI pro BaseAddress.");
				}
			}
			else
			{
				throw new InvalidOperationException("HttpContext není dostupný.");
			}

			return await client.PostAsJsonAsync(endpoint, content);
		}

		public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T content)
		{
			var client = _httpClientFactory.CreateClient();
			var request = _httpContextAccessor.HttpContext?.Request;

			if (request != null)
			{
				var baseAddress = $"{request.Scheme}://{request.Host.Value}/";
				if (Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
				{
					client.BaseAddress = uri;
				}
				else
				{
					throw new InvalidOperationException("Neplatný formát URI pro BaseAddress.");
				}
			}
			else
			{
				throw new InvalidOperationException("HttpContext není dostupný.");
			}

			return await client.PutAsJsonAsync(endpoint, content);
		}

		public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
		{
			var client = _httpClientFactory.CreateClient();
			var request = _httpContextAccessor.HttpContext?.Request;

			if (request != null)
			{
				var baseAddress = $"{request.Scheme}://{request.Host.Value}/";
				if (Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
				{
					client.BaseAddress = uri;
				}
				else
				{
					throw new InvalidOperationException("Neplatný formát URI pro BaseAddress.");
				}
			}
			else
			{
				throw new InvalidOperationException("HttpContext není dostupný.");
			}

			return await client.DeleteAsync(endpoint);
		}
	}
}
