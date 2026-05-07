using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.UseCases.Firebase;

namespace MyInventoryApp.src.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenFirebaseController : BaseController
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
            return FromResult(result);
        }
    }
}
