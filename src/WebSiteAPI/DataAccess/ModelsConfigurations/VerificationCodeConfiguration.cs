using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess.ModelsConfigurations;

/// <summary>
/// 
/// </summary>
public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.ToTable("VerificationCodes");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.UserId)
            .IsRequired();

        builder.Property(v => v.Code)
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(v => v.CreatedAt)
            .IsRequired();

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<VerificationCode>(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 