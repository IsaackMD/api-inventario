using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Mappers;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;
using System.Data;

namespace MyInventoryApp.src.Application.UseCases.Products
{
    public class ListProduct
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper; // Cambia MappingProfile por IMapper

        public ListProduct(
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
                return Result<IEnumerable<ProductoDTO>>.Success([]);

            // Mapeo a DTO usando IMapper
            var Mapper = _mapper.Map<IEnumerable<ProductoDTO>>(products);

            return Result<IEnumerable<ProductoDTO>>.Success(Mapper);
        }
    }
}
