using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Application.UseCases.AlertaLowProductCase;
using MyInventoryApp.src.Application.UseCases.InfoData;

namespace MyInventoryApp.src.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResumenDashboardController : BaseController
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
            return FromResult( result );
        }

        [HttpGet("low-products")]
        public async Task<IActionResult> GetLowProducts()
        {
             var result = await _lowProductsUseCase.ExecuteAsync();
            return FromResult(result);
        }
    }
}
