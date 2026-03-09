using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;
using MyInventoryApp.src.Infraestructure.Persistence.Repositories;

namespace MyInventoryApp.src.Application.UseCases.Products
{
    public class IncreaseStockUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockMovementRepository _movementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncreaseStockUseCase(
        IProductRepository productRepository,
        IStockMovementRepository movementRepository,
        IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<String>> ExecuteAsync(Guid productId, int quantity)
        {
            
            var product = await _productRepository.GetByIdAsync(productId);
            

            if (product is null) return Result<String>.Failure("Producto no encontrado");

            

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                int oldStock = product.Stock;
                product.IncreaseStock(quantity);

                var movement = new StockMovement(
                    product.Id,
                    oldStock,
                    quantity,
                    StockMovementType.In
                );
                await _movementRepository.AddAsync(movement);
                await _productRepository.UpdateAsync(product);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync(); 
                
                return Result<String>.Success("Producto Actualizado");
            }
            catch (Exception ex) {

                await _unitOfWork.RollbackAsync();
                return Result<String>.Failure(ex.Message);
            }
        }
    }
}
