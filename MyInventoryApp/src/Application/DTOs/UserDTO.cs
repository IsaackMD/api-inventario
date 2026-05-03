namespace MyInventoryApp.src.Application.DTOs
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class AuthUserDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }

    public class UserLoginDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class ResponseLoginDTO
    {
        public string? Token { get; set; }
        public AuthUserDTO? user
        {
            get; set;
        }
    }

    public class UserClaims
    {
        public string userId { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}
