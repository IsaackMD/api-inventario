using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Domain.Entities;


namespace MyInventoryApp.src.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name)
                 )
                .ForMember(
                    dest => dest.Stockmin,
                    opt => opt.MapFrom(src => src.StockMin)
                 );
            CreateMap<ProductDTO, Product>();

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


            CreateMap<User, AuthUserDTO>()
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => src.RolType)
                );

        }
    }
}
