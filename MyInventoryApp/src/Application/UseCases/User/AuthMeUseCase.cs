using MyInventoryApp.src.Application.DTOs;
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

        public async Task<Result<UserClaims>> Execute(string token)
        {

            if (string.IsNullOrEmpty(token))
                return Result<UserClaims>.Failure("Token es requerido.");

            var claims = _tokenValidator.Validate(token);

            if (claims == null)
                return Result<UserClaims>.Failure("Token Invalido.");

            var user = new UserClaims
            {
                userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                email = claims.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
                name = claims.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty
            };

            return Result<UserClaims>.Success(user);

        }
    }
}
