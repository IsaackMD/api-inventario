using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.DTOs
{
    public class ProductDTO : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? Stock { get; set; }
        public int? Stockmin { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }


    public class ProductRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
