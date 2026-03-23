using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Firebase
{
    public class FirebaseUseCase
    {
        private readonly INotificationTokenRepository _notificationTokenRepository;

        public FirebaseUseCase(INotificationTokenRepository notificationTokenRepository)
        {
            _notificationTokenRepository = notificationTokenRepository;
        }

        public async Task<Result<string>> Execute()
        {
            try
            {
                var token = await _notificationTokenRepository.GetTokeFirebase();
                if (string.IsNullOrEmpty(token))
                {
                    return Result<string>.Failure("No se encontró el token de Firebase.");
                }
                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Error al obtener el token de Firebase: {ex.Message}");
            }
        }
    }
}
