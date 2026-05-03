using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Application.DTOs;
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
        }

        public async Task<StockMovement?> GetByIdAsync(Guid id)
        {
            return await _context.StockMovements.FindAsync(id);
        }

        public async Task<IEnumerable<StockDTO>> GetAllAsync()
        {
            return await _context.StockMovements
                .AsNoTracking()
                .Include(s => s.Product)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new StockDTO
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    Producto = new ProductoDTO
                    {
                        id = s.Product.Id,
                        name = s.Product.Name,
                        description = s.Product.Description,
                    },
                    OldStock = s.OldStock,
                    Quantity = s.Quantity,
                    MovementType = s.Type.ToString(),
                    MovementDate = s.CreatedAt
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(StockMovement stock)
        {
            _context.StockMovements.Update(stock);
        }
        public async Task DeleteAsync(Guid id)
        {
            var stock = await GetByIdAsync(id);
            if (stock == null) return;

            _context.StockMovements.Remove(stock);
        }

        public async Task<StockMovement?> GetStockByProduct(Guid Id)
        {
            return await _context.StockMovements.FirstOrDefaultAsync(s => s.ProductId == Id);
        }

        public async Task<List<StockDTO>> GetLastMovements()
        {
            return await _context.StockMovements
                .AsNoTracking()
                .Include(s => s.Product)
                .OrderByDescending(s => s.CreatedAt)
                .Take(10)
                .Select(s => new StockDTO
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    Producto = new ProductoDTO
                    {
                        id = s.Product.Id,
                        name = s.Product.Name,
                        description = s.Product.Description
                    },
                    Quantity = s.Quantity,
                    MovementType = s.Type.ToString(),
                    MovementDate = s.CreatedAt
                })
                .ToListAsync();
        }
    }
}