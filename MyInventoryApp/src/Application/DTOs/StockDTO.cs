namespace MyInventoryApp.src.Application.DTOs
{
    public class StockDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; } // e.g., "IN" or "OUT"
    }
}
