using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess.ModelsConfigurations;

/// <summary>
/// 
/// </summary>
public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.ToTable("Stories");

        builder.HasKey(s => s.Id);
        
        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(s => s.PreviewImageUrl)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.HasMany(s => s.StoryItems)
            .WithOne()
            .HasForeignKey("StoryId")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Story>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 