using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess.ModelsConfigurations;

/// <summary>
/// 
/// </summary>
public class StoryItemConfiguration : IEntityTypeConfiguration<StoryItem>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<StoryItem> builder)
    {
        builder.ToTable("StoryItems");

        builder.HasKey(si => si.Id);

        builder.Property(si => si.StoryId)
            .IsRequired();

        builder.Property(si => si.SourceUrl)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(si => si.CreatedAt)
            .IsRequired();

        builder.HasOne<Story>()
            .WithMany(s => s.StoryItems)
            .HasForeignKey(si => si.StoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 