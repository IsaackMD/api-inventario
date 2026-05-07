using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.UseCases.Stocks;

namespace MyInventoryApp.src.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class StockController : BaseController
    {
        private readonly ListStockUseCase _useCase;

        public StockController(
            ListStockUseCase useCase
        )
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var result = await _useCase.ExecuteAsync();
            return FromResult(result);
        }

        [HttpGet]
        [Route("Product")]
        public async Task<IActionResult> GetStockProduct(Guid Id)
        {
            var result = await _useCase.ExecuteSingle(Id);
            return FromResult(result);
        }

        [HttpGet]
        [Route("LastMovements")]
        public async Task<IActionResult> GetLastMovements()
        {
            var result = await _useCase.ExecuteLastMovements();
            return FromResult(result);
        }
    }

}
