using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface IStockMovementRepository
    {
        Task AddAsync(StockMovement stock);
        Task<StockMovement?> GetByIdAsync(Guid id);
        Task<IEnumerable<StockMovement>> GetAllAsync();
        Task UpdateAsync(StockMovement stock);
        Task DeleteAsync(Guid id);
        Task<StockMovement?> GetStockByProduct(Guid Id);
    }
}
