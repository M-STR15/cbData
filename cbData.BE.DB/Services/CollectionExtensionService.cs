using Microsoft.Extensions.DependencyInjection;

namespace cbData.BE.DB.Services
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddCbDataBeDbServices(this IServiceCollection services)
		{
			services.AddScoped<ProductDbService>();
			services.AddScoped<ReportDbService>();
			return services;
		}
	}
}
