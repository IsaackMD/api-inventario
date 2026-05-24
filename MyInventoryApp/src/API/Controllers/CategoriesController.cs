using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.Categories;

namespace MyInventoryApp.src.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly CreateCategoryUseCase _useCase;
        private readonly UpdateCategoryUseCase _useUpdateCategory;
        private readonly ListCategoryUseCase _useListCategory;

        public CategoriesController(CreateCategoryUseCase useCase,
            ListCategoryUseCase useListCategory,
            UpdateCategoryUseCase useUpdateCategory
        )
        {
            _useCase = useCase;
            _useListCategory = useListCategory;
            _useUpdateCategory = useUpdateCategory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO dto)
        {
            var result = await _useCase.Execute(dto);
            return FromCreated(result, nameof(GetCategories));
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _useListCategory.ExecuteAsync();
            return FromResult(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CategoryDTO dto)
        {
            var result = await _useUpdateCategory.Execute(dto);
            return FromResult(result);
        }

        [HttpPut]
        [Route("status")]
        public async Task<IActionResult> Update(CategoryStatusDTO dto)
        {
            var result = await _useUpdateCategory.ExecuteStatus(dto);

            return FromResult(result);
        }
    }
}
