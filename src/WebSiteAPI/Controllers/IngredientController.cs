using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления ингредиентами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class IngredientController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса IngredientController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public IngredientController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех ингредиентов
    /// </summary>
    /// <returns>Список ингредиентов с информацией о продуктах</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
    {
        return await _context.Ingredients
            .Include(i => i.Products)
            .ToListAsync();
    }

    /// <summary>
    /// Получает ингредиент по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ингредиента</param>
    /// <returns>Ингредиент с информацией о продуктах</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Ingredient>> GetIngredient(Guid id)
    {
        var ingredient = await _context.Ingredients
            .Include(i => i.Products)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (ingredient == null)
        {
            return NotFound();
        }

        return ingredient;
    }

    /// <summary>
    /// Создает новый ингредиент
    /// </summary>
    /// <param name="ingredient">Данные нового ингредиента</param>
    /// <returns>Созданный ингредиент</returns>
    [HttpPost]
    public async Task<ActionResult<Ingredient>> CreateIngredient(Ingredient ingredient)
    {
        ingredient.Id = Guid.NewGuid();
        ingredient.CreatedAt = DateTime.UtcNow;
        ingredient.UpdatedAt = DateTime.UtcNow;

        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetIngredient), new { id = ingredient.Id }, ingredient);
    }

    /// <summary>
    /// Обновляет существующий ингредиент
    /// </summary>
    /// <param name="id">Идентификатор ингредиента</param>
    /// <param name="ingredient">Обновленные данные ингредиента</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIngredient(Guid id, Ingredient ingredient)
    {
        if (id != ingredient.Id)
        {
            return BadRequest();
        }

        ingredient.UpdatedAt = DateTime.UtcNow;
        _context.Entry(ingredient).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IngredientExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет ингредиент
    /// </summary>
    /// <param name="id">Идентификатор ингредиента</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngredient(Guid id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient == null)
        {
            return NotFound();
        }

        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool IngredientExists(Guid id)
    {
        return _context.Ingredients.Any(e => e.Id == id);
    }
} 