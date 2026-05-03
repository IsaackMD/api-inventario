using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.Infraestructure.Persistence.Configurations
{
    public class NotificationTokenConfiguration : IEntityTypeConfiguration<NotificationToken>
    {
        public void Configure(EntityTypeBuilder<NotificationToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}