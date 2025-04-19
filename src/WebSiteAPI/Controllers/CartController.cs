using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления корзинами пользователей
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса CartController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех корзин
    /// </summary>
    /// <returns>Список корзин с информацией о пользователях и элементах корзины</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
    {
        return await _context.Carts
            .Include(c => c.UserId)
            .Include(c => c.CartItems)
            .ToListAsync();
    }

    /// <summary>
    /// Получает корзину по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор корзины</param>
    /// <returns>Корзина с информацией о пользователе и элементах корзины</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetCart(Guid id)
    {
        var cart = await _context.Carts
            .Include(c => c.UserId)
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cart == null)
        {
            return NotFound();
        }

        return cart;
    }

    /// <summary>
    /// Создает новую корзину
    /// </summary>
    /// <param name="cart">Данные новой корзины</param>
    /// <returns>Созданная корзина</returns>
    [HttpPost]
    public async Task<ActionResult<Cart>> CreateCart(Cart cart)
    {
        cart.Id = Guid.NewGuid();
        cart.CreatedAt = DateTime.UtcNow;
        cart.UpdatedAt = DateTime.UtcNow;

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
    }

    /// <summary>
    /// Обновляет существующую корзину
    /// </summary>
    /// <param name="id">Идентификатор корзины</param>
    /// <param name="cart">Обновленные данные корзины</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCart(Guid id, Cart cart)
    {
        if (id != cart.Id)
        {
            return BadRequest();
        }

        cart.UpdatedAt = DateTime.UtcNow;
        _context.Entry(cart).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CartExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет корзину
    /// </summary>
    /// <param name="id">Идентификатор корзины</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(Guid id)
    {
        var cart = await _context.Carts.FindAsync(id);
        if (cart == null)
        {
            return NotFound();
        }

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CartExists(Guid id)
    {
        return _context.Carts.Any(e => e.Id == id);
    }
} 