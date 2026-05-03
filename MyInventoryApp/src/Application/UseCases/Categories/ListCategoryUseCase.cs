using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Categories
{
    public class ListCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper; // Cambia MappingProfile por IMapper

        public ListCategoryUseCase(
            ICategoryRepository categoryRepository,
            IMapper mapper // Cambia MappingProfile por IMapper
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CategoryDTO>>> ExecuteAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            // Mapeo a DTO usando IMapper
            return Result<IEnumerable<CategoryDTO>>.Success(
                _mapper.Map<IEnumerable<CategoryDTO>>(categories)
                );
        }
    }
}
