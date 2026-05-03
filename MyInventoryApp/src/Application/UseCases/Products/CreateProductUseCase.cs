using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Products
{
    public class CreateProductUseCase
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStockMovementService _stockMovementService;

        public CreateProductUseCase(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IStockMovementRepository stockMovementRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStockMovementService stockMovementService
            )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _stockMovementRepository = stockMovementRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stockMovementService = stockMovementService;

        }

        public async Task<Result<ProductoDTO>> Execute(ProductoDTO dto)
        {
            if (dto.stock <= 0)
                return Result<ProductoDTO>.Failure("Stock no puede ser negativo");


            if (dto.CategoryId == null)
            {
                return Result<ProductoDTO>.Failure("CategoryId es requerida");
            }
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
            if (category == null)
            {
                return Result<ProductoDTO>.Failure("Categoria No encontrada");
            }


            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var product = new Product
                (
                   dto.name,
                   dto.description,
                   dto.stock ?? 0,
                   dto.stockmin ?? 0,
                   category
                );
                await _productRepository.AddAsync(product);

                await _stockMovementService.RegisterAsync(
                    product,
                    0,
                    dto.stock ?? 0,
                    StockMovementType.In
                );

                await _unitOfWork.CommitAsync();

                var Mapper = _mapper.Map<ProductoDTO>(product);

                return Result<ProductoDTO>.Success(Mapper);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Result<ProductoDTO>.Failure("Error al crear el producto");
            }
        }
    }
}
