namespace WebSiteAPI.Models;

/// <summary>
/// Корзина пользователя
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Элементы корзины
    /// </summary>
    public List<CartItem> CartItems { get; set; }

    /// <summary>
    /// Токен корзины
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Общая сумма корзины
    /// </summary>
    public double TotalAmount { get; set; }
}