using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}
