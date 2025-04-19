using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления пользователями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса UserController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех пользователей
    /// </summary>
    /// <returns>Список пользователей с информацией о корзине и коде верификации</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users
            .Include(u => u.CartId)
            .Include(u => u.VerificationCodeId)
            .Include(u => u.Orders)
            .ToListAsync();
    }

    /// <summary>
    /// Получает пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Пользователь с информацией о корзине и коде верификации</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.CartId)
            .Include(u => u.VerificationCodeId)
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    /// <summary>
    /// Создает нового пользователя
    /// </summary>
    /// <param name="user">Данные нового пользователя</param>
    /// <returns>Созданный пользователь</returns>
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    /// <summary>
    /// Обновляет существующего пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="user">Обновленные данные пользователя</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        user.UpdatedAt = DateTime.UtcNow;
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(Guid id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
} 