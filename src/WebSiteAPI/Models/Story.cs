namespace WebSiteAPI.Models;

/// <summary>
/// История (Story)
/// </summary>
public class Story : BaseEntity
{
    /// <summary>
    /// URL превью изображения
    /// </summary>
    public string PreviewImageUrl { get; set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Элементы истории
    /// </summary>
    public List<StoryItem> StoryItems { get; set; }
}