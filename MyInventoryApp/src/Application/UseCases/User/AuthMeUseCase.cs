using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;
using System.Security.Claims;


namespace MyInventoryApp.src.Application.UseCases.User
{
    public class AuthMeUseCase
    {
        private readonly ITokenValidator _tokenValidator;

        public AuthMeUseCase(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }

        public async Task<Result<object>> Execute(string token)
        {

            if (string.IsNullOrEmpty(token))
                return Result<object>.Failure("Token es requerido.");

            var claims = _tokenValidator.Validater(token);

            if (claims == null)
                return Result<object>.Failure("Token Invalido.");

            var user = new
            {
                userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                email = claims.FindFirst(ClaimTypes.Email)?.Value,
                name = claims.FindFirst(ClaimTypes.Name)?.Value
            };

            return Result<object>.Success(user);

        }
    }
}
