using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Entities;


namespace MyInventoryApp.src.Infraestructure.Persistence
{
    public class MyInventoryDbContext : DbContext
    {
        public MyInventoryDbContext(DbContextOptions<MyInventoryDbContext> options) : base(options)
        {
        }

        // DbSets para las entidades del dominio
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyInventoryDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
