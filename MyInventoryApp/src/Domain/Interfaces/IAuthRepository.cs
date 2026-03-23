using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<Result<string>> Login(string email, string password);
        Task<Result<User>> CreateUser(User user);

    }
}
