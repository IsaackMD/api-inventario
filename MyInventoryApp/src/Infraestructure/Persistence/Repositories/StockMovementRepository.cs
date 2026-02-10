using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Infraestructure.Persistence.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly MyInventoryDbContext _context;

        public StockMovementRepository(MyInventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StockMovement stock)
        {
            await _context.StockMovements.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<StockMovement?> GetByIdAsync(Guid id)
        {
            return await _context.StockMovements.FindAsync(id);
        }

        public async Task<IEnumerable<StockMovement>> GetAllAsync()
        {
            return await _context.StockMovements.ToListAsync();
        }

        public async Task UpdateAsync(StockMovement stock)
        {
            _context.StockMovements.Update(stock);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var stock = await GetByIdAsync(id);
            if (stock == null) return;

            _context.StockMovements.Remove(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<StockMovement?> GetStockByProduct(Guid Id)
        {
            return await _context.StockMovements.FirstOrDefaultAsync(s => s.ProductId == Id);
        }
    }
}