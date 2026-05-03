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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStockMovementService _stockMovementService;

        public CreateProductUseCase(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStockMovementService stockMovementService
            )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stockMovementService = stockMovementService;

        }

        public async Task<Result<ProductDTO>> Execute(ProductDTO dto)
        {
            if (dto.Stock <= 0)
                return Result<ProductDTO>.Failure("Stock no puede ser negativo");


            if (dto.CategoryId == null)
            {
                return Result<ProductDTO>.Failure("CategoryId es requerida");
            }
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
            if (category == null)
            {
                return Result<ProductDTO>.Failure("Categoria No encontrada");
            }


            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var product = new Product
                (
                   dto.Name,
                   dto.Description,
                   dto.Stock ?? 0,
                   dto.Stockmin ?? 0,
                   category
                );
                await _productRepository.AddAsync(product);

                await _stockMovementService.RegisterAsync(
                    product,
                    0,
                    dto.Stock ?? 0,
                    StockMovementType.In
                );

                await _unitOfWork.CommitAsync();

                var Mapper = _mapper.Map<ProductDTO>(product);

                return Result<ProductDTO>.Success(Mapper);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Result<ProductDTO>.Failure("Error al crear el producto");
            }
        }
    }
}
