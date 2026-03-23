namespace MyInventoryApp.src.Domain.Entities
{
    public class Credenciales
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Credencial { get; private set; }

        protected Credenciales() { }

        public Credenciales(string code, string credencial)
        {
            Id = Guid.NewGuid();
            Code = code;
            Credencial = credencial;
        }
    }
}
