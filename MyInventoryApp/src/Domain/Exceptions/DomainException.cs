
namespace MyInventoryApp.src.Domain.Exceptions
{
    public class DomainException : Exception
    {
        protected DomainException(string messageError) : base(messageError) { }
    }

    public class InvalidStockException : DomainException
    {
        public InvalidStockException(string messageError) : base(messageError) { }
    }

    public class InsufficientStockException : DomainException
    {
        public InsufficientStockException()
            : base("Stock insuficiente para realizar la operación") { }
    }
}
