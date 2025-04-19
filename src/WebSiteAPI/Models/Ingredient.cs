namespace WebSiteAPI.Models;

/// <summary>
/// Ингредиент
/// </summary>
public class Ingredient : BaseEntity
{
    /// <summary>
    /// Наименование ингредиента
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена ингредиента
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// URL изображения ингредиента
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// Продукты, связанные с этим ингредиентом
    /// </summary>
    public List<Product> Products { get; set; }

    /// <summary>
    /// Элементы корзины, связанные с этим ингредиентом
    /// </summary>
    public List<CartItem> CartItems { get; set; }
}