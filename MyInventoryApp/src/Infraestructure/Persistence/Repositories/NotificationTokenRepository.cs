using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Interfaces;
using System;

namespace MyInventoryApp.src.Infraestructure.Persistence.Repositories
{
    public class NotificationTokenRepository : INotificationTokenRepository
    {
        private readonly MyInventoryDbContext _context;
        public NotificationTokenRepository(MyInventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllTokensAsync()
        {
            return await _context.NotificationToken
                .Select(x => x.Token)
                .ToListAsync();
        }

        public async Task<string> GetTokeFirebase()
        {
            var token = await _context.Credenciales
                .Where(c => c.Code == "vapidKey").
                Select(c => c.Credencial)
                .FirstOrDefaultAsync();
            return token ?? string.Empty;
        }
    }
}
