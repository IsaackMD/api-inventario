using MyInventoryApp.src.Application.DTOs;
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

        public async Task Execute(CategoryDTO dto)
        {
            var category = new Category(dto.name);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _categoriaRepository.AddAsync(category);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
            
        }
    }
}
