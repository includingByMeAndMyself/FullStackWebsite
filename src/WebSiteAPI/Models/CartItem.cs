namespace WebSiteAPI.Models;

/// <summary>
/// Элемент корзины
/// </summary>
public class CartItem : BaseEntity
{
    /// <summary>
    /// Id корзины
    /// </summary>
    public Guid CartId { get; set; }
    
    /// <summary>
    /// Id элемента продукта
    /// </summary>
    public Guid ProductItemId { get; set; }

    /// <summary>
    /// Количество элементов
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Ингредиенты, добавленные к элементу
    /// </summary>
    public List<Ingredient>? Ingredients { get; set; }
}