namespace MyInventoryApp.src.Application.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public bool isDelete { get; set; }
    }

    public class CategoryStatusDTO
    {
        public Guid Id { get; set; }
        public bool isDelete { get; set; }
    }
}
