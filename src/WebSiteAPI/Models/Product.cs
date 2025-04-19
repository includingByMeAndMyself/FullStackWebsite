
namespace WebSiteAPI.Models;

/// <summary>
/// Продукт магазина
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Наименование продукта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// URL изображения продукта
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Id Ингредиентов продукта
    /// </summary>
    public List<Ingredient> Ingredients { get; set; }

    /// <summary>
    /// Элементы продукта
    /// </summary>
    public List<ProductItem> ProductItems { get; set; }

    /// <summary>
    /// Id категории
    /// </summary>
    public Guid CategoryId { get; set; }
}
