using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Infraestructure.Persistence.Repositories
{
    public class GetInfoRepository
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
                totalProducto = totalProductos,
                totalStock = totalStock,
                stockBajos = stockBajos,
                totalCategorias = totalCategorias
            };
        }

        public async Task<List<AlertaLowProductDTO>> GetLowProducts()
        {
            var lowProducts = await _context.Products
                .Where(p => p.Stock <= p.StockMin)
                .Select(p => new AlertaLowProductDTO
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
