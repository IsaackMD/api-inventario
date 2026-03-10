namespace MyInventoryApp.src.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public bool IsDeleted { get; set; }
        protected Category() { }

        public Category(string name, string description, bool isDeleted)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsDeleted = isDeleted;
        }
    }
}
