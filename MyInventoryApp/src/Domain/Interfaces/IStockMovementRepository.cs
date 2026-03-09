using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface IStockMovementRepository
    {
        Task AddAsync(StockMovement stock);
        Task<StockMovement?> GetByIdAsync(Guid id);
        Task<IEnumerable<StockDTO>> GetAllAsync();
        Task UpdateAsync(StockMovement stock);
        Task DeleteAsync(Guid id);
        Task<StockMovement?> GetStockByProduct(Guid Id);
        Task<List<StockDTO>> GetLastMovements();
    }
}
