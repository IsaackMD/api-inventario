using Microsoft.AspNetCore.Routing.Constraints;

namespace MyInventoryApp.src.Application.DTOs
{
    public class ProductoDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
        public int? stock { get; set; }
        public int? stockmin { get; set; }
        public Guid? CategoryId { get; set; }
        public string? categoryName { get; set; }
    }


    public class ProductRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
