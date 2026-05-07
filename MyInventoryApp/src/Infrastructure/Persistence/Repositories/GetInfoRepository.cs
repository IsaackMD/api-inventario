using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Infrastructure.Persistence.Repositories
{
    public class GetInfoRepository : IGetInfoRepository
    {

        private readonly MyInventoryDbContext _context;

        public GetInfoRepository(MyInventoryDbContext context)
        {
            _context = context;
        }

        public async Task<DataDTO> GetCountDashboard()
        {
            var totalProductos = await _context.Products.CountAsync();

            var totalStock = await _context.StockMovements
                .SumAsync(m => m.Type == StockMovementType.In ? m.Quantity : -m.Quantity);

            var stockBajos = await _context.Products
                .Where(p => p.Stock <= p.StockMin)
                .CountAsync();
            var totalCategorias = await _context.Categories.CountAsync();

            return new DataDTO
            {
                TotalProducto = totalProductos,
                TotalStock = totalStock,
                StockBajos = stockBajos,
                TotalCategorias = totalCategorias
            };
        }

        public async Task<IEnumerable<AlertLowProductDTO>> GetLowProducts()
        {
            var lowProducts = await _context.Products
                .AsNoTracking()
                .Where(p => p.Stock <= p.StockMin)
                .Select(p => new AlertLowProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stock = p.Stock,
                    StockMin = p.StockMin
                })
                .ToListAsync();
            return lowProducts;
        }

    }

}
