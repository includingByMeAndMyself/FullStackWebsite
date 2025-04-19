using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления элементами корзины
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartItemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса CartItemController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public CartItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех элементов корзины
    /// </summary>
    /// <returns>Список элементов корзины с информацией о корзине и продукте</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
    {
        return await _context.CartItems
            .Include(ci => ci.CartId)
            .Include(ci => ci.ProductItemId)
            .ToListAsync();
    }

    /// <summary>
    /// Получает элемент корзины по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор элемента корзины</param>
    /// <returns>Элемент корзины с информацией о корзине и продукте</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CartItem>> GetCartItem(Guid id)
    {
        var cartItem = await _context.CartItems
            .Include(ci => ci.CartId)
            .Include(ci => ci.ProductItemId)
            .FirstOrDefaultAsync(ci => ci.Id == id);

        if (cartItem == null)
        {
            return NotFound();
        }

        return cartItem;
    }

    /// <summary>
    /// Создает новый элемент корзины
    /// </summary>
    /// <param name="cartItem">Данные нового элемента корзины</param>
    /// <returns>Созданный элемент корзины</returns>
    [HttpPost]
    public async Task<ActionResult<CartItem>> CreateCartItem(CartItem cartItem)
    {
        cartItem.Id = Guid.NewGuid();
        cartItem.CreatedAt = DateTime.UtcNow;
        cartItem.UpdatedAt = DateTime.UtcNow;

        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
    }

    /// <summary>
    /// Обновляет существующий элемент корзины
    /// </summary>
    /// <param name="id">Идентификатор элемента корзины</param>
    /// <param name="cartItem">Обновленные данные элемента корзины</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(Guid id, CartItem cartItem)
    {
        if (id != cartItem.Id)
        {
            return BadRequest();
        }

        cartItem.UpdatedAt = DateTime.UtcNow;
        _context.Entry(cartItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CartItemExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет элемент корзины
    /// </summary>
    /// <param name="id">Идентификатор элемента корзины</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartItem(Guid id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null)
        {
            return NotFound();
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CartItemExists(Guid id)
    {
        return _context.CartItems.Any(e => e.Id == id);
    }
} 