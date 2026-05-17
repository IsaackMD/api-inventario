using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Products
{
    public class EditProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditProductUseCase(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductDTO>> ExecuteAsync(ProductDTO dto)
        {
            var productEntity = _mapper.Map<Product>(dto);
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _productRepository.UpdateAsync(productEntity);
                await _unitOfWork.CommitAsync();
                return Result<ProductDTO>.Success(dto);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Result<ProductDTO>.Failure($"Error al actualizar el producto");

            }



        }

        public async Task<Result<string>> ExecuteChangeStatus(ProductStatusChangeRequest dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product == null) return Result<string>.Failure($"Producto con ID {dto.ProductId} no encontrado");

            product.ChangeStatus(dto.IsActive);
            try
            {
                Console.WriteLine("Begin transaction");
                await _unitOfWork.BeginTransactionAsync();
                Console.WriteLine("Updating product");
                await _productRepository.UpdateAsync(product);
                Console.WriteLine("Commit");
                await _unitOfWork.CommitAsync();
                return Result<string>.Success($"Estado del producto {(dto.IsActive ? "activado" : "desactivado")} exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace); await _unitOfWork.RollbackAsync();
                return Result<string>.Failure($"Error al cambiar el estado del producto");

            }
        }

    }
}
