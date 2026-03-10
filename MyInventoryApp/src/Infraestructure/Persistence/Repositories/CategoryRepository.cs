using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Infraestructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoriaRepository
    {
        private readonly MyInventoryDbContext _context;

        public CategoryRepository(MyInventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.AddAsync(category);
        }

        public async Task<Category> GetByIdAsync(Guid id) =>
            await _context.Categories.FindAsync(id);

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _context.Categories
            .Where(c => c.IsDeleted == false)
            .ToListAsync();

        public async Task UpdateAsync(Category category)
        {
             _context.Update(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);
            if (category == null) return;

            _context.Categories.Remove(category);
        }
    }

}
