using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Categories
{
    public class UpdateCategoryUseCase
    {
        private readonly ICategoryRepository _categoriaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryUseCase(
            ICategoryRepository categoriaRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Execute(CategoryDTO dto)
        {
            if (dto.Id == Guid.Empty)
            {
                return Result<string>.Failure("Id de categoria no valido");
            }
            var entityCategory = _mapper.Map<Category>(dto);
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _categoriaRepository.UpdateAsync(entityCategory);
                await _unitOfWork.CommitAsync();
                return Result<string>.Success("Categoria actualizada correctamente");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return Result<string>.Failure("Error al actualizar la categoria");
            }
        }

        public async Task<Result<String>> ExecuteStatus(CategoryStatusDTO dto)
        {
            var category = await _categoriaRepository.GetByIdAsync(dto.Id);
            if (category == null)
            {
                return Result<string>.Failure("Categoria no encontrada");
            }
            category.Disable();
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _categoriaRepository.UpdateAsync(category);
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
