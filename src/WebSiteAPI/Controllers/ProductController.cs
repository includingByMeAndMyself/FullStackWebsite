using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления продуктами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса ProductController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех продуктов
    /// </summary>
    /// <returns>Список продуктов с информацией о категории, ингредиентах и элементах продукта</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products
            .Include(p => p.CategoryId)
            .Include(p => p.Ingredients)
            .Include(p => p.ProductItems)
            .ToListAsync();
    }

    /// <summary>
    /// Получает продукт по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор продукта</param>
    /// <returns>Продукт с информацией о категории, ингредиентах и элементах продукта</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.CategoryId)
            .Include(p => p.Ingredients)
            .Include(p => p.ProductItems)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    /// <summary>
    /// Создает новый продукт
    /// </summary>
    /// <param name="product">Данные нового продукта</param>
    /// <returns>Созданный продукт</returns>
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    /// <summary>
    /// Обновляет существующий продукт
    /// </summary>
    /// <param name="id">Идентификатор продукта</param>
    /// <param name="product">Обновленные данные продукта</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        product.UpdatedAt = DateTime.UtcNow;
        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет продукт
    /// </summary>
    /// <param name="id">Идентификатор продукта</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(Guid id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
} 