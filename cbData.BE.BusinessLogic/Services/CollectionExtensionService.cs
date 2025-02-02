using cbData.BE.BusinessLogic.Controllers;
using cbData.BE.DB.Services;
using Microsoft.Extensions.DependencyInjection;

namespace cbData.BE.BusinessLogic.Services
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddCbDataBeBusinessLogicService(this IServiceCollection services)
		{
			services.AddCbDataBeDbServices();
			services.AddScoped<ProductController>();

			return services;
		}
	}
}