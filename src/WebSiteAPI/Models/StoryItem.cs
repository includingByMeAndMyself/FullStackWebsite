namespace WebSiteAPI.Models;

/// <summary>
/// Элемент истории
/// </summary>
public class StoryItem : BaseEntity
{
    /// <summary>
    /// Id истории
    /// </summary>
    public Guid StoryId { get; set; }
    
    /// <summary>
    /// URL источника элемента
    /// </summary>
    public string SourceUrl { get; set; }
}