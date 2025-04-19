namespace WebSiteAPI.Models;

/// <summary>
/// Заказ пользователя
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Токен заказа
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Общая сумма заказа
    /// </summary>
    public double TotalAmount { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Идентификатор платежа
    /// </summary>
    public Guid PaymentId { get; set; }

    /// <summary>
    /// Элементы заказа
    /// </summary>
    public string Items { get; set; }

    /// <summary>
    /// Полное имя получателя
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Email получателя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Телефон получателя
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Комментарий к заказу
    /// </summary>
    public string Comment { get; set; }
}