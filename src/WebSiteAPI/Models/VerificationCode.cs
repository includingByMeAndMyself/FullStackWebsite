namespace WebSiteAPI.Models;

/// <summary>
/// Код верификации пользователя
/// </summary>
public class VerificationCode : BaseEntity
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Код верификации
    /// </summary>
    public string Code { get; set; }
}