using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyInventoryApp.src.Infrastructure.Service.Jwt
{
    public abstract class JwtBaseService
    {
        protected readonly string _secret;
        protected readonly SymmetricSecurityKey _key;


        protected JwtBaseService(IConfiguration configuration)
        {
            _secret = configuration["Jwt:Secret"]
           ?? throw new Exception("JWT_SECRET no está configurado");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        }
    }
}
