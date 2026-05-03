using Microsoft.EntityFrameworkCore.Storage;
using MyInventoryApp.src.Domain.Interfaces;
using MyInventoryApp.src.Infraestructure.Persistence;

namespace MyInventoryApp.src.Infraestructure
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly MyInventoryDbContext _context;
        private IDbContextTransaction? _transaction;

        public EfUnitOfWork(MyInventoryDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction!.RollbackAsync();
        }


    }
}