using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.FirebaseServices
{
    public class FirebaseNotificationService : INotificationService
    {
        public async Task SendAsync(string token, string title, string body)
        {
            var message = new FirebaseAdmin.Messaging.Message
            {
                Token = token,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                }
            };

            await FirebaseAdmin.Messaging.FirebaseMessaging
                .DefaultInstance
                .SendAsync(message);
        }
    }
}
