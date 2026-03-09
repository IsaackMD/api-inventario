using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;
using System.Xml;

namespace MyInventoryApp.src.Application.UseCases.Categories
{
    public class CreateCategoryUseCase
    {

        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryUseCase(
            ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork    )
        {
            _categoriaRepository = categoriaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CategoryDTO>> Execute(CategoryDTO dto)
        {
            var category = new Category(dto.name,dto?.description);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _categoriaRepository.AddAsync(category);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();


                dto.Id = category.Id;
                return Result<CategoryDTO>.Success(dto);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return Result<CategoryDTO>.Failure("Ocurrio un error durante la creación de la categoria");
            }
            
        }
    }
}
