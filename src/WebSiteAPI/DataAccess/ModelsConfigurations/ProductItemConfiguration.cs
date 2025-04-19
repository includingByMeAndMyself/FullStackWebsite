using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess.ModelsConfigurations;

/// <summary>
/// 
/// </summary>
public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.ToTable("ProductItems");

        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.Price)
            .IsRequired();

        builder.Property(pi => pi.Size)
            .IsRequired(false);

        builder.Property(pi => pi.ProductType)
            .IsRequired(false);

        builder.Property(pi => pi.ProductId)
            .IsRequired();

        builder.HasOne<Product>()
            .WithMany(p => p.ProductItems)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pi => pi.CartItems)
            .WithOne()
            .HasForeignKey(ci => ci.ProductItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}