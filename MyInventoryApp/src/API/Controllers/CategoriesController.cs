using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.Categories;

namespace MyInventoryApp.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
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
            await _useCase.Execute(dto);
            return Ok();
        }
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return await _useListCategory.ExecuteAsync();
        }

        [HttpPut]
        [Route("status")]
        public async Task<IActionResult> Update(CategoryStatusDTO dto)
        {
            var result = await _useUpdateCategory.ExecuteStatus(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
    }
}
