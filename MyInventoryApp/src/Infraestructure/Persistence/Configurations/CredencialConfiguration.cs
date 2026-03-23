using Microsoft.EntityFrameworkCore;
using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Infraestructure.Persistence.Configurations
{
    public class CredencialConfiguration: IEntityTypeConfiguration<Credenciales>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Credenciales> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever(); // Asume que el Id es Asignado por el constructor de la entidad
            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Credencial)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
