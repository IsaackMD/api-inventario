using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Mappers;
using MyInventoryApp.src.Domain.Interfaces;
using System.Data;

namespace MyInventoryApp.src.Application.UseCases.Categories
{
    public class ListCategoryUseCase
    {
        private readonly ICategoriaRepository _categoryRepository;
        private readonly IMapper _mapper; // Cambia MappingProfile por IMapper

        public ListCategoryUseCase(
            ICategoriaRepository categoryRepository,
            IMapper mapper // Cambia MappingProfile por IMapper
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> ExecuteAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            // Mapeo a DTO usando IMapper
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
    }
}
