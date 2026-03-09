using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Infraestructure.Persistence.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                  .ValueGeneratedNever();

            builder.Property(s => s.Quantity)
                  .IsRequired();

            builder.Property(s => s.Type)
                   .IsRequired();

            builder.Property(s => s.CreatedAt)
                   .IsRequired();

            builder.HasOne(s => s.Product)
               .WithMany()
               .HasForeignKey(s => s.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
