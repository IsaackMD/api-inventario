using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Products
{
    public class IncreaseStockUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockMovementRepository _movementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockMovementService _stockMovementService;

        public IncreaseStockUseCase(
        IProductRepository productRepository,
        IStockMovementRepository movementRepository,
        IUnitOfWork unitOfWork,
        IStockMovementService stockMovementService
        )
        {
            _productRepository = productRepository;
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
            _stockMovementService = stockMovementService;
        }

        public async Task<Result<String>> ExecuteAsync(Guid productId, int quantity)
        {
            if (quantity <= 0)
                return Result<String>.Failure("La cantidad debe ser mayor a cero");

            var product = await _productRepository.GetByIdAsync(productId);


            if (product is null) return Result<String>.Failure("Producto no encontrado");



            await _unitOfWork.BeginTransactionAsync();

            try
            {
                int oldStock = product.Stock;
                product.IncreaseStock(quantity);

                await _stockMovementService.RegisterAsync(
                    product,
                    oldStock,
                    quantity,
                    StockMovementType.In
                );

                await _productRepository.UpdateAsync(product);

                await _unitOfWork.CommitAsync();

                return Result<String>.Success("Producto Actualizado");
            }
            catch (Exception ex)
            {

                await _unitOfWork.RollbackAsync();
                return Result<String>.Failure("Error al aumentar el stock");
            }
        }
    }
}
