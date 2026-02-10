using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Products
{
    public class DecreaseStockUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DecreaseStockUseCase(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<String>> ExecuteAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null) return Result<String>.Failure("Product no encontrado.");
        
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                product.DecreaseStock(quantity);
                await _productRepository.UpdateAsync(product);
                await _unitOfWork.CommitAsync();
                return Result<String>.Success("Stock disminuido correctamente.");
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result<String>.Failure($"Error al disminuir el stock: {ex.Message}");
            }
        }
    }
}
