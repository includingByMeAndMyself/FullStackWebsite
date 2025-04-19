namespace WebSiteAPI.Models;

/// <summary>
/// Категория продуктов
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Наименование категории
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Продукты в категории
    /// </summary>
    public List<Product> Products { get; set; }
}