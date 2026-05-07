using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.User;
using static Google.Apis.Requests.BatchRequest;

namespace MyInventoryApp.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly AuthMeUseCase _authMeUseCase;
        public UserController(
            CreateUserUseCase createUserUseCase,
            LoginUseCase loginUseCase,
            AuthMeUseCase authMeUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _loginUseCase = loginUseCase;
            _authMeUseCase = authMeUseCase;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO dto)
        {
            var result = await _createUserUseCase.Execute(dto);
            
            return FromResult(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO authUserDTO)
        {
            var result = await _loginUseCase.Execute(authUserDTO);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            var token = result?.Value?.Token;
            if(token is null)
                return BadRequest("Error al generar el token");

            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,          // 🔥 evita acceso desde JS (seguridad)
                SameSite = SameSiteMode.Lax,
                Secure = true,
                MaxAge = TimeSpan.FromHours(2)
            });

            return Ok(new
            {
                success = true,
                data = new
                {
                    result?.Value?.user,
                }
            });
        }
        [Authorize]
        [HttpGet]
        [Route("auth/me")]
        public async Task<IActionResult> AuthMe()
        {
            var token = Request.Cookies["access_token"];
            if(token is null)
                return BadRequest("Token no encontrado en las cookies");

            var result = await _authMeUseCase.Execute(token);

            return FromResult(result);
        }
    }
}
