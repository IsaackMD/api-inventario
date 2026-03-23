using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace MyInventoryApp.FirebaseServices
{
    public class FirebaseService
    {
        public FirebaseService()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("../inventario-f5f60-firebase-adminsdk-fbsvc-9c52206661.json")
            });
        }

        public async Task SendNotification(string token, string title, string body)
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
