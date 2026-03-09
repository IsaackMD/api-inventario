using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Infraestructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MyInventoryDbContext _context;

    public ProductRepository(MyInventoryDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);

    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
             .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
    }
    public async Task DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);
        if (product == null) return;
        
       _context.Products.Remove(product);
    }
}
