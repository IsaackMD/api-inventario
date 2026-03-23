namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface INotificationTokenRepository
    {
        Task<List<string>> GetAllTokensAsync();
        Task<string> GetTokeFirebase();
    }
}