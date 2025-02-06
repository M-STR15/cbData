#pragma warning disable IDE0058

using cbData.BE.BusinessLogic.Controllers;
using cbData.BE.BusinessLogic.Helpers;
using cbData.BE.DB.Services;
using Microsoft.Extensions.DependencyInjection;

namespace cbData.BE.BusinessLogic.Services
{
	public static class CollectionExtensionService
	{
		public static IServiceCollection AddCbDataBeBusinessLogicService(this IServiceCollection services)
		{
			services.AddCbDataBeDbServices();

			services.AddAutoMapper(typeof(MappingProfile));

			services.AddSingleton	<ProductController>();

			return services;
		}
	}
}