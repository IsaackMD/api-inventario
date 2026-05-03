using System.Security.Claims;

namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface ITokenValidator
    {
        public ClaimsPrincipal? Validate(string token);
    }
}
