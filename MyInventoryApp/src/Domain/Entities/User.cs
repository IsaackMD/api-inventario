namespace MyInventoryApp.src.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected User() { }

        public User(string name, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }
        // Método para actualizar el nombre del usuario
        public void UpdateName(string newName)
        {
            Name = newName;
        }
        // Método para actualizar el correo electrónico del usuario
        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
        }
        // Método para actualizar la contraseña del usuario
        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }
    }
}
