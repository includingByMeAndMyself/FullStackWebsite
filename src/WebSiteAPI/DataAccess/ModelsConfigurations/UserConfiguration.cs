using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess.ModelsConfigurations;

/// <summary>
/// 
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256)
            .IsUnicode(false);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.Verified)
            .IsRequired(false);

        builder.Property(u => u.Provider)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.CartId)
            .IsRequired();

        builder.Property(u => u.VerificationCodeId)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired();

        builder.HasOne<Cart>()
            .WithOne()
            .HasForeignKey<User>(u => u.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<VerificationCode>()
            .WithOne()
            .HasForeignKey<User>(u => u.VerificationCodeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Orders)
            .WithOne()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}