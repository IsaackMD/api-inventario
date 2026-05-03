using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Notify
{
    public class NotifyLowStockUseCase
    {
        private readonly INotificationTokenRepository _tokenRepository;
        private readonly INotificationService _notificationService;

        public NotifyLowStockUseCase(
            INotificationTokenRepository tokenRepository,
            INotificationService notificationService)
        {
            _tokenRepository = tokenRepository;
            _notificationService = notificationService;
        }

        public async Task Execute(ProductDTO product)
        {
            if (product.Stock > product.Stockmin)
                return;

            var tokens = await _tokenRepository.GetAllTokensAsync();

            foreach (var token in tokens)
            {
                await _notificationService.SendAsync(
                    token,
                    "Stock bajo",
                    $"El producto {product.Name} tiene poco stock"
                );
            }
        }
    }
}
