using Microsoft.IdentityModel.Tokens;
using MyInventoryApp.src.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyInventoryApp.src.Infraestructure.Service.Jwt
{
    public class TokenValidator : JwtBaseService, ITokenValidator
    {

        public TokenValidator(IConfiguration configuration) : base(configuration) { }

        public ClaimsPrincipal? Validate(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ClockSkew = TimeSpan.Zero
                };
                return handler.ValidateToken(token, parameters, out _);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
