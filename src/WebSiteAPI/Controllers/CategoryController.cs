using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления категориями продуктов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса CategoryController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех категорий
    /// </summary>
    /// <returns>Список категорий с информацией о продуктах</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();
    }

    /// <summary>
    /// Получает категорию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <returns>Категория с информацией о продуктах</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    /// <summary>
    /// Создает новую категорию
    /// </summary>
    /// <param name="category">Данные новой категории</param>
    /// <returns>Созданная категория</returns>
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        if (string.IsNullOrEmpty(category.Name))
        {
            return BadRequest("Name is required");
        }

        if (await _context.Categories.AnyAsync(c => c.Name == category.Name))
        {
            return BadRequest("Category with this name already exists");
        }

        category.Id = Guid.NewGuid();
        category.CreatedAt = DateTime.UtcNow;
        category.UpdatedAt = DateTime.UtcNow;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    /// <summary>
    /// Обновляет существующую категорию
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="category">Обновленные данные категории</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(category.Name))
        {
            return BadRequest("Name is required");
        }

        if (await _context.Categories.AnyAsync(c => c.Name == category.Name && c.Id != id))
        {
            return BadRequest("Category with this name already exists");
        }

        category.UpdatedAt = DateTime.UtcNow;
        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет категорию
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(Guid id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
} 