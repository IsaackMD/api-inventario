namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(string token, string title, string body);
    }
}
