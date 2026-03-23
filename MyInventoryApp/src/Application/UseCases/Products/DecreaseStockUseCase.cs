using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Products
{
    public class DecreaseStockUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockMovementRepository _movementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DecreaseStockUseCase(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IStockMovementRepository movementRepository,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ProductoDTO>> ExecuteAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null) return Result<ProductoDTO>.Failure("Product no encontrado.");
        
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                int oldStock = product.Stock;
                product.DecreaseStock(quantity);

                var movement = new StockMovement(
                    product.Id,
                    oldStock,
                    quantity,
                    StockMovementType.Out
                );
                await _movementRepository.AddAsync(movement);
                await _productRepository.UpdateAsync(product);


                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                var mapper = _mapper.Map<ProductoDTO>(product);
                return Result<ProductoDTO>.Success(mapper);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result<ProductoDTO>.Failure($"Error al disminuir el stock: {ex.Message}");
            }
        }
    }
}
