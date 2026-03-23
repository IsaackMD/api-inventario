namespace MyInventoryApp.src.Domain.Entities
{
    public class NotificationToken
    {
        public Guid Id { get; private set; }
        public Guid UserId  { get; private set; }
        public User User { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected NotificationToken() { }
        public NotificationToken(Guid userId, string token)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            CreatedAt = DateTime.UtcNow;
        }
        
        public void UpdateToken(string newToken)
        {
            Token = newToken;
            CreatedAt = DateTime.UtcNow; // Actualiza la fecha de creación al actualizar el token
        }
    }
}