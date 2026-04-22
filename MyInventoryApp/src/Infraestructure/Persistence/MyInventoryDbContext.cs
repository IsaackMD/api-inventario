using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Entities;
using MyInventoryApp.src.Infraestructure.Persistence.Configurations;


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
        public DbSet<Credenciales> Credenciales => Set<Credenciales>();
        public DbSet<NotificationToken> NotificationToken => Set<NotificationToken>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyInventoryDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new CredencialConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationTokenConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
