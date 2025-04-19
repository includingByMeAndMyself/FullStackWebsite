namespace WebSiteAPI.Models;

/// <summary>
/// Базовая сущность
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Id сущности
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Дата создания сущности
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления сущности
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}