using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления заказами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса OrderController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех заказов
    /// </summary>
    /// <returns>Список заказов с информацией о пользователе и элементах заказа</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders
            .Include(o => o.UserId)
            .ToListAsync();
    }

    /// <summary>
    /// Получает заказ по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <returns>Заказ с информацией о пользователе и элементах заказа</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(Guid id)
    {
        var order = await _context.Orders
            .Include(o => o.UserId)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    /// <summary>
    /// Создает новый заказ
    /// </summary>
    /// <param name="order">Данные нового заказа</param>
    /// <returns>Созданный заказ</returns>
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        order.Id = Guid.NewGuid();
        order.CreatedAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    /// <summary>
    /// Обновляет существующий заказ
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <param name="order">Обновленные данные заказа</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        order.UpdatedAt = DateTime.UtcNow;
        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет заказ
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrderExists(Guid id)
    {
        return _context.Orders.Any(e => e.Id == id);
    }
} 