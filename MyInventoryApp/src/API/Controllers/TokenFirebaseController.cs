using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.Categories;
using MyInventoryApp.src.Application.UseCases.Firebase;

namespace MyInventoryApp.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenFirebaseController : ControllerBase
    {
        private readonly FirebaseUseCase _useCase;


        public TokenFirebaseController(FirebaseUseCase useCase
        )
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetTokenFirebase()
        {
            var result = await _useCase.Execute();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
    }
}
