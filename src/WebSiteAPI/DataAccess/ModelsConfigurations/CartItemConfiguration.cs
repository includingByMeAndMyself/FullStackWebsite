using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess.ModelsConfigurations;

/// <summary>
/// 
/// </summary>
public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.CartId)
            .IsRequired();

        builder.Property(ci => ci.ProductItemId)
            .IsRequired();

        builder.Property(ci => ci.Quantity)
            .IsRequired();

        builder.Property(ci => ci.CreatedAt)
            .IsRequired();

        builder.Property(ci => ci.UpdatedAt)
            .IsRequired();

        builder.HasMany(ci => ci.Ingredients)
            .WithMany(i => i.CartItems)
            .UsingEntity(j => j.ToTable("CartItemIngredients"));

        builder.HasOne<ProductItem>()
            .WithMany()
            .HasForeignKey(ci => ci.ProductItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Cart>()
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 