using AutoMapper;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;
using MyInventoryApp.src.Infraestructure.Persistence.Repositories;

namespace MyInventoryApp.src.Application.UseCases.Products
{
    public class CreateProductUseCase
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoriaRepository _categoryRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductUseCase(
            IProductRepository productRepository,
            ICategoriaRepository categoryRepository,
            IStockMovementRepository stockMovementRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _stockMovementRepository = stockMovementRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<ProductoDTO>> Execute(ProductoDTO dto)
        {
            if(dto.CategoryId == null)
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

                var stockMovement = new StockMovement
                (
                    product.Id,
                    0,
                    dto.stock ?? 0,
                    StockMovementType.In
                );

                await _stockMovementRepository.AddAsync(stockMovement);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                var Mapper = _mapper.Map <ProductoDTO>(product);

                return Result<ProductoDTO>.Success(Mapper);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result<ProductoDTO>.Failure(ex.Message);
            }
        }
    }
}
