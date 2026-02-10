using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;
using System.Xml;

namespace MyInventoryApp.src.Application.UseCases.Categories
{
    public class CreateCategoryUseCase
    {

        private readonly ICategoriaRepository _categoriaRepository;

        public CreateCategoryUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Execute(CategoryDTO dto)
        {
            var category = new Category(dto.name);

            await _categoriaRepository.AddAsync(category);
        }
    }
}
