using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Infrastructure.Service
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IStockMovementRepository _movementRepository;
        public StockMovementService(IStockMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }
        public async Task RegisterAsync(
            Product product,
            int oldStock,
            int quantity,
            StockMovementType movementType
            )
        {
            var movement = new StockMovement(
                product.Id,
                oldStock,
                quantity,
                movementType
            );

            await _movementRepository.AddAsync(movement);

        }
    }
}
