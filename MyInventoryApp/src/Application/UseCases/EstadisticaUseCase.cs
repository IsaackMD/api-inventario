using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases
{
    public class EstadisticaUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper; // Cambia MappingProfile por IMapper

        public EstadisticaUseCase(
            IProductRepository productRepository,
            IMapper mapper // Cambia MappingProfile por IMapper
            )
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductoDTO>>> ExecuteAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (!products.Any())
                return Result<IEnumerable<ProductoDTO>>.Failure("No hay articulos");
            // Mapeo a DTO usando IMapper
            var Mapper = _mapper.Map<IEnumerable<ProductoDTO>>(products);

            return Result<IEnumerable<ProductoDTO>>.Success(Mapper);
        }
    }
}
