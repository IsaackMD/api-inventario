using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface IStockMovementService
    {
        Task RegisterAsync(
            Product product,
            int oldStock,
            int quantity,
            StockMovementType movementType
            );
    }
}
