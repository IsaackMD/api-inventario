using MyInventoryApp.src.Domain.Exceptions;

namespace MyInventoryApp.src.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Stock { get; private set; }

        public int StockMin { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        protected Product() { }

        public Product(string name, string description, int initialStock, int minStock, Category category)
        {
            if (initialStock <= 0)
                throw new InvalidStockException("El stock inicial debe ser mayor a cero.");

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Stock = initialStock;
            StockMin = minStock;
            Category = category;
        }


        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidStockException("La cantidad a incrementar debe ser mayor a cero.");

            Stock += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidStockException("La cantidad a incrementar debe ser mayor a cero.");

            if (Stock < quantity)
                throw new InsufficientStockException();

            Stock -= quantity;
        }
    }
}
