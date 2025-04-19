namespace WebSiteAPI.Models;

/// <summary>
/// Элемент продукта
/// </summary>
public class ProductItem : BaseEntity
{
    /// <summary>
    /// Цена элемента продукта
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Размер элемента продукта
    /// </summary>
    public int? Size { get; set; }

    /// <summary>
    /// Тип продукта (если применимо)
    /// </summary>
    public int? ProductType { get; set; }

    /// <summary>
    /// Элементы корзины, связанные с этим элементом продукта
    /// </summary>
    public List<CartItem> CartItems { get; set; }
    
    /// <summary>A
    /// Id продукта
    /// </summary>
    public Guid ProductId { get; set; }
}