using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IAuthRepository
    {
        private readonly MyInventoryDbContext _context;
        public UserRepository(MyInventoryDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }
        public async Task<User?> LoginAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;

        }

        public async Task<bool> ExistEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
