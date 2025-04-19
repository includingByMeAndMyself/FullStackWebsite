using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления кодами верификации
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VerificationCodeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса VerificationCodeController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public VerificationCodeController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех кодов верификации
    /// </summary>
    /// <returns>Список кодов верификации с информацией о пользователе</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VerificationCode>>> GetVerificationCodes()
    {
        return await _context.VerificationCodes
            .Include(vc => vc.UserId)
            .ToListAsync();
    }

    /// <summary>
    /// Получает код верификации по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор кода верификации</param>
    /// <returns>Код верификации с информацией о пользователе</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<VerificationCode>> GetVerificationCode(Guid id)
    {
        var verificationCode = await _context.VerificationCodes
            .Include(vc => vc.UserId)
            .FirstOrDefaultAsync(vc => vc.Id == id);

        if (verificationCode == null)
        {
            return NotFound();
        }

        return verificationCode;
    }

    /// <summary>
    /// Создает новый код верификации
    /// </summary>
    /// <param name="verificationCode">Данные нового кода верификации</param>
    /// <returns>Созданный код верификации</returns>
    [HttpPost]
    public async Task<ActionResult<VerificationCode>> CreateVerificationCode(VerificationCode verificationCode)
    {
        verificationCode.Id = Guid.NewGuid();
        verificationCode.CreatedAt = DateTime.UtcNow;
        verificationCode.UpdatedAt = DateTime.UtcNow;

        _context.VerificationCodes.Add(verificationCode);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVerificationCode), new { id = verificationCode.Id }, verificationCode);
    }

    /// <summary>
    /// Обновляет существующий код верификации
    /// </summary>
    /// <param name="id">Идентификатор кода верификации</param>
    /// <param name="verificationCode">Обновленные данные кода верификации</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVerificationCode(Guid id, VerificationCode verificationCode)
    {
        if (id != verificationCode.Id)
        {
            return BadRequest();
        }

        verificationCode.UpdatedAt = DateTime.UtcNow;
        _context.Entry(verificationCode).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VerificationCodeExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет код верификации
    /// </summary>
    /// <param name="id">Идентификатор кода верификации</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVerificationCode(Guid id)
    {
        var verificationCode = await _context.VerificationCodes.FindAsync(id);
        if (verificationCode == null)
        {
            return NotFound();
        }

        _context.VerificationCodes.Remove(verificationCode);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VerificationCodeExists(Guid id)
    {
        return _context.VerificationCodes.Any(e => e.Id == id);
    }
} 