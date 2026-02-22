using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.InfoData;

namespace MyInventoryApp.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumenDashboardController : ControllerBase
    {
        private readonly GetInfoUseCase _useCase;

        public ResumenDashboardController(GetInfoUseCase useCase)
        {
            _useCase = useCase;
        }


        [HttpGet]
        public async Task<DataDTO> GetResumen()
        {
            return await _useCase.ExecuteAsync();
        }
    }
}
