using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Domain.Entities;


namespace MyInventoryApp.src.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Product, ProductoDTO>()
                .ForMember(
                    dest => dest.categoryName,
                    opt => opt.MapFrom(src => src.Category.Name)
                 )
                .ForMember(
                    dest => dest.stockmin,
                    opt => opt.MapFrom(src => src.StockMin)
                 );
            CreateMap<ProductoDTO, Product>();

            // Mapper Categories
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            // Mapper Stock
            CreateMap<StockMovement, StockDTO>()
                .ForMember(
                    dest => dest.MovementType,
                    opt => opt.MapFrom(src => src.Type.ToString())
                )
                .ForMember(
                    dest => dest.MovementDate,
                    opt => opt.MapFrom(src => src.CreatedAt)
                );
            CreateMap<StockDTO, StockMovement>()
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.Ignore()  // ← CreatedAt es private set, se establece en el constructor
            )
            .ForMember(
                dest => dest.ProductId,
                opt => opt.Ignore()  // ← ProductId es private set
            );

        }
    }
}
