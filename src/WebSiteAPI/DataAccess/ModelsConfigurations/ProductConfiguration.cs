using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.API.Models;

namespace Store.API.DataAccess.ModelsConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);

        builder.Property(p => p.Stock)
            .HasDefaultValue(0)
            .IsRequired();
        
        builder.Property(p => p.Locations)
            .HasDefaultValue(0)
            .IsRequired();
    }
}