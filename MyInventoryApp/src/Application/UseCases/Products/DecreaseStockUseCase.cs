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
        private readonly IStockMovementService _stockMovementService;
        public DecreaseStockUseCase(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IStockMovementRepository movementRepository,
            IMapper mapper,
            IStockMovementService stockMovementService
            )
        {
            _productRepository = productRepository;
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stockMovementService = stockMovementService;
        }
        public async Task<Result<ProductoDTO>> ExecuteAsync(Guid productId, int quantity)
        {
            if (quantity <= 0)
                return Result<ProductoDTO>.Failure("La cantidad debe ser mayor a cero.");

            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null)
                return Result<ProductoDTO>.Failure("Product no encontrado.");

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                int oldStock = product.Stock;

                product.DecreaseStock(quantity);

                await _stockMovementService.RegisterAsync(
                    product,
                    oldStock,
                    quantity,
                    StockMovementType.Out
                    );

                await _productRepository.UpdateAsync(product);

                await _unitOfWork.CommitAsync();

                return Result<ProductoDTO>.Success(_mapper.Map<ProductoDTO>(product));
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Result<ProductoDTO>.Failure("Error al disminuir el stock");
            }
        }
    }
}
