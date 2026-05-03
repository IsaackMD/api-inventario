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

        public async Task<Result<IEnumerable<ProductDTO>>> ExecuteAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (!products.Any())
                return Result<IEnumerable<ProductDTO>>.Success([]);

            // Mapeo a DTO usando IMapper
            var Mapper = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Result<IEnumerable<ProductDTO>>.Success(Mapper);
        }
    }
}
