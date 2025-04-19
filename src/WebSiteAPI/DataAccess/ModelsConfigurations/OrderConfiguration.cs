using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess.ModelsConfigurations;

/// <summary>
/// 
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.Token)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(o => o.TotalAmount)
            .IsRequired();

        builder.Property(o => o.Status)
            .IsRequired();

        builder.Property(o => o.PaymentId)
            .IsRequired();

        builder.Property(o => o.Items)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(o => o.FullName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(o => o.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(o => o.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(o => o.Address)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(o => o.Comment)
            .HasMaxLength(1000);

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.Property(o => o.UpdatedAt)
            .IsRequired();
        
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Order>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 