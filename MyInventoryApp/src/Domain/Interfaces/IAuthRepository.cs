using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> CreateUser(User user);
        Task<User?> LoginAsync(string email);
        Task<User?> ExistEmail(string email);
    }
}
