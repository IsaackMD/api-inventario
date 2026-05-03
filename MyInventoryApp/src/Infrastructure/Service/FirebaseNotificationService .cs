using FirebaseAdmin.Messaging;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Infrastructure.Service
{
    public class FirebaseNotificationService : INotificationService
    {
        public async Task SendAsync(string token, string title, string body)
        {
            var message = new Message()
            {
                Token = token,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                }
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
    }
}
