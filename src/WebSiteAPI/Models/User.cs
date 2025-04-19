namespace WebSiteAPI.Models;

/// <summary>
/// Пользователь системы
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Полное имя пользователя
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Дата верификации пользователя
    /// </summary>
    public DateTime? Verified { get; set; }

    /// <summary>
    /// Провайдер авторизации (например, Google)
    /// </summary>
    public Provider Provider { get; set; }
    
    /// <summary>
    /// Id Корзины пользователя
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Заказы пользователя
    /// </summary>
    public List<Order> Orders { get; set; }

    /// <summary>
    /// Id Код верификации пользователя
    /// </summary>
    public Guid VerificationCodeId { get; set; }
}