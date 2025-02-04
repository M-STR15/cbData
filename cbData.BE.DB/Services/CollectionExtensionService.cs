#pragma warning disable IDE0058

using Microsoft.Extensions.DependencyInjection;

namespace cbData.BE.DB.Services
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddCbDataBeDbServices(this IServiceCollection services)
		{
			services.AddSingleton<ProductDbService>();
			services.AddScoped<ReportDbService>();
			return services;
		}
	}
}