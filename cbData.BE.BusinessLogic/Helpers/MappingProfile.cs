using AutoMapper;
using AutoMapper.EquivalencyExpression;
using cbData.BE.BusinessLogic.Models.Products;
using cbData.BE.BusinessLogic.Models.Reports;
using cbData.BE.DB.Models.Products;
using cbData.BE.DB.Models.Reports;

namespace cbData.BE.BusinessLogic.Helpers
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{
			CreateMap<Order, OrderDto>().ReverseMap()
				.EqualityComparison((entity, dto) => entity.Id == dto.Id);
			CreateMap<Order, OrderBaseDto>().ReverseMap()
				.EqualityComparison((entity, dto) => entity.Id == dto.Id);
			CreateMap<Order, OrderAddBaseDto>().ReverseMap();

			CreateMap<Product, ProductDto>().ReverseMap()
				.EqualityComparison((entity, dto) => entity.Id == dto.Id);
			CreateMap<Product, ProductBaseDto>().ReverseMap()
				.EqualityComparison((entity, dto) => entity.Id == dto.Id);
			CreateMap<Product, ProductAddBaseDto>().ReverseMap();

			CreateMap<TotalOrdersByProduct, TotalOrdersByProductDto>().ReverseMap()
				.EqualityComparison((entity, dto) => entity.Product.Id == dto.Product.Id);
		}
	}
}
