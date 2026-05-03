using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.AlertaLowProductCase;
using MyInventoryApp.src.Application.UseCases.InfoData;

namespace MyInventoryApp.src.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResumenDashboardController : ControllerBase
    {
        private readonly GetInfoUseCase _useCase;
        private readonly AlertaLowProductCase _lowProductsUseCase;

        public ResumenDashboardController(
            GetInfoUseCase useCase,
            AlertaLowProductCase lowProductsUseCase
            )
        {
            _useCase = useCase;
            _lowProductsUseCase = lowProductsUseCase;
        }


        [HttpGet]
        public async Task<IActionResult> GetResumen()
        {
            var result = await _useCase.ExecuteAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("low-products")]
        public async Task<List<AlertLowProductDTO>> GetLowProducts()
        {
            return await _lowProductsUseCase.ExecuteAsync();
        }
    }
}
