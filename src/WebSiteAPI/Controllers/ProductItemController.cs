using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления элементами продукта
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductItemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса ProductItemController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public ProductItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех элементов продукта
    /// </summary>
    /// <returns>Список элементов продукта с информацией о продукте</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductItem>>> GetProductItems()
    {
        return await _context.ProductItems
            .Include(pi => pi.ProductId)
            .Include(c => c.CartItems)
            .ToListAsync();
    }

    /// <summary>
    /// Получает элемент продукта по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор элемента продукта</param>
    /// <returns>Элемент продукта с информацией о продукте</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductItem>> GetProductItem(Guid id)
    {
        var productItem = await _context.ProductItems
            .Include(pi => pi.ProductId)
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(pi => pi.Id == id);

        if (productItem == null)
        {
            return NotFound();
        }

        return productItem;
    }

    /// <summary>
    /// Создает новый элемент продукта
    /// </summary>
    /// <param name="productItem">Данные нового элемента продукта</param>
    /// <returns>Созданный элемент продукта</returns>
    [HttpPost]
    public async Task<ActionResult<ProductItem>> CreateProductItem(ProductItem productItem)
    {
        productItem.Id = Guid.NewGuid();
        productItem.CreatedAt = DateTime.UtcNow;
        productItem.UpdatedAt = DateTime.UtcNow;
        
        _context.ProductItems.Add(productItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProductItem), new { id = productItem.Id }, productItem);
    }

    /// <summary>
    /// Обновляет существующий элемент продукта
    /// </summary>
    /// <param name="id">Идентификатор элемента продукта</param>
    /// <param name="productItem">Обновленные данные элемента продукта</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductItem(Guid id, ProductItem productItem)
    {
        if (id != productItem.Id)
        {
            return BadRequest();
        }

        productItem.UpdatedAt = DateTime.UtcNow;
        _context.Entry(productItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductItemExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет элемент продукта
    /// </summary>
    /// <param name="id">Идентификатор элемента продукта</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductItem(Guid id)
    {
        var productItem = await _context.ProductItems.FindAsync(id);
        if (productItem == null)
        {
            return NotFound();
        }

        _context.ProductItems.Remove(productItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductItemExists(Guid id)
    {
        return _context.ProductItems.Any(e => e.Id == id);
    }
} 