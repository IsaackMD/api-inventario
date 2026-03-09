namespace MyInventoryApp.src.Domain.Entities
{
    public class StockMovement
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int OldStock { get; set; }
        public int Quantity { get; set; }
        public StockMovementType Type { get; set; }
        public DateTime CreatedAt { get; private set; }

        public StockMovement()
        {
        }

        public StockMovement(Guid productoId,int oldquantity, int quantity, StockMovementType type)
        {
            if (quantity <= 0)
                throw new ArgumentException("La Cantidad debe ser mayor a cero.");

            Id = Guid.NewGuid();
            ProductId = productoId;
            Quantity = quantity;
            OldStock = oldquantity;
            Type = type;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
