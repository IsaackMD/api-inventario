using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Infraestructure.Persistence.Repositories
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

        public async Task<User?> ExistEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
