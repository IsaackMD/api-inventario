using MyInventoryApp.src.Domain.Interfaces;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;

namespace MyInventoryApp.src.Application.UseCases.Categories
{
    public class UpdateCategoryUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryUseCase(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
        {
            _categoriaRepository = categoriaRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<String>> ExecuteStatus(CategoryStatusDTO dto)
        {
            var category = await _categoriaRepository.GetByIdAsync(dto.Id);
            if (category == null)
            {
                return Result<string>.Failure("Categoria no encontrada");
            }
            category.IsDeleted =dto.isDelete;
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _categoriaRepository.UpdateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return Result<string>.Success("Categoria actualizada correctamente");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return Result<string>.Failure("Error al actualizar la categoria");
            }
        }
    }
}
