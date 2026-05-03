namespace MyInventoryApp.src.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsDeleted { get; private set; }
        protected Category() { }

        public Category(string name, string description, bool isDeleted)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsDeleted = isDeleted;
        }

        public void Disable() => IsDeleted = true;
        public void Enable() => IsDeleted = false;
    }
}
