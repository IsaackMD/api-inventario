using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Categories
{
    public class GetProductsUseCase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductsUseCase(
            IProductRepository repository, IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ProductoDTO>> GetProducts(Guid Id)
        {
            if (Id == Guid.Empty) return Result<ProductoDTO>.Failure("El Id esta vacio");
            var Producto = await _repository.GetByIdAsync(Id);

            if(Producto == null ) return Result<ProductoDTO>.Failure("No existe el producto");
            var mapping = _mapper.Map<ProductoDTO>(Producto);

            return Result<ProductoDTO>.Success(mapping);
        }
    }
}
