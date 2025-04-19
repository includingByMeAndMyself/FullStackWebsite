using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess.ModelsConfigurations;

/// <summary>
/// 
/// </summary>
public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("Ingredients");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(i => i.Price)
            .IsRequired();

        builder.Property(i => i.ImageUrl)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(i => i.CreatedAt)
            .IsRequired();

        builder.Property(i => i.UpdatedAt)
            .IsRequired();

        builder.HasMany(i => i.Products)
            .WithMany(p => p.Ingredients)
            .UsingEntity(j => j.ToTable("ProductIngredients"));

        builder.HasMany(i => i.CartItems)
            .WithMany(ci => ci.Ingredients)
            .UsingEntity(j => j.ToTable("CartItemIngredients"));
    }
}