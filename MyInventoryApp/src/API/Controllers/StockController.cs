using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.UseCases.Stocks;

namespace MyInventoryApp.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StockController : ControllerBase
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
            if (!result.IsSuccess)
                return NotFound(new { message = result.Error });

            return Ok(result);
        }

        [HttpGet]
        [Route("Product")]
        public async Task<IActionResult> GetStockProduct(Guid Id)
        {
            var result = await _useCase.ExecuteSingle(Id);
            if (!result.IsSuccess)
                return NotFound(new { message = result.Error });

            return Ok(result);
        }

        [HttpGet]
        [Route("LastMovements")]
        public async Task<IActionResult> GetLastMovements()
        {
            var result = await _useCase.ExecuteLastMovements();
            if (!result.IsSuccess)
                return NotFound(new { message = result.Error });
            return Ok(result);
        }
    }

}
