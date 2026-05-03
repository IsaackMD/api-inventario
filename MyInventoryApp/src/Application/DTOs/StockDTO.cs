namespace MyInventoryApp.src.Application.DTOs
{

    public class StockDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int OldStock { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; } // e.g., "IN" or "OUT"
    }

    public class DataDTO
    {
        public int TotalProducto { get; set; }
        public int TotalStock { get; set; }
        public int StockBajos { get; set; }

        public int TotalCategorias { get; set; }
    }

    public class AlertLowProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int StockMin { get; set; }

    }
}
