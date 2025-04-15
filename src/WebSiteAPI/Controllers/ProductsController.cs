using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.API.DataAccess;
using Store.API.Models;

namespace Store.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение списка всех продуктов
    /// </summary>
    /// <returns>Список продуктов</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    /// <summary>
    /// Получение продукта по ID
    /// </summary>
    /// <param name="id">ID продукта</param>
    /// <returns>Продукт с указанным ID</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    /// <summary>
    /// Создание нового продукта
    /// </summary>
    /// <param name="product">Данные нового продукта</param>
    /// <returns>Созданный продукт</returns>
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    /// <summary>
    /// Обновление значения продукта
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(Guid id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Удалить продукт
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
}