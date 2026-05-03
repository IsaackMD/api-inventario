using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.User;

namespace MyInventoryApp.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
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
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
            var token = result.Value.Token;
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,          // 🔥 evita acceso desde JS (seguridad)
                SameSite = SameSiteMode.Lax,
                Secure = true,
                MaxAge = TimeSpan.FromHours(2)
            });

            result.Value.Token = null; // No devolver el token en el cuerpo de la respuesta

            return Ok(result);
        }

        [HttpGet]
        [Route("auth/me")]
        public async Task<IActionResult> AuthMe()
        {
            var token = Request.Cookies["access_token"];

            var result = await _authMeUseCase.Execute(token);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Value);
        }
    }
}
