using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления элементами историй
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StoryItemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса StoryItemController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public StoryItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех элементов историй
    /// </summary>
    /// <returns>Список элементов историй с информацией о связанной истории</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoryItem>>> GetStoryItems()
    {
        return await _context.StoryItems
            .Include(si => si.StoryId)
            .ToListAsync();
    }

    /// <summary>
    /// Получает элемент истории по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор элемента истории</param>
    /// <returns>Элемент истории с информацией о связанной истории</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<StoryItem>> GetStoryItem(Guid id)
    {
        var storyItem = await _context.StoryItems
            .Include(si => si.StoryId)
            .FirstOrDefaultAsync(si => si.Id == id);

        if (storyItem == null)
        {
            return NotFound();
        }

        return storyItem;
    }

    /// <summary>
    /// Создает новый элемент истории
    /// </summary>
    /// <param name="storyItem">Данные нового элемента истории</param>
    /// <returns>Созданный элемент истории</returns>
    [HttpPost]
    public async Task<ActionResult<StoryItem>> CreateStoryItem(StoryItem storyItem)
    {
        if (string.IsNullOrEmpty(storyItem.SourceUrl))
        {
            return BadRequest("SourceUrl is required");
        }

        if (!await _context.Stories.AnyAsync(s => s.Id == storyItem.StoryId))
        {
            return BadRequest("Story with specified StoryId does not exist");
        }

        storyItem.Id = Guid.NewGuid();
        storyItem.CreatedAt = DateTime.UtcNow;
        storyItem.UpdatedAt = DateTime.UtcNow;

        _context.StoryItems.Add(storyItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStoryItem), new { id = storyItem.Id }, storyItem);
    }

    /// <summary>
    /// Обновляет существующий элемент истории
    /// </summary>
    /// <param name="id">Идентификатор элемента истории</param>
    /// <param name="storyItem">Обновленные данные элемента истории</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStoryItem(Guid id, StoryItem storyItem)
    {
        if (id != storyItem.Id)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(storyItem.SourceUrl))
        {
            return BadRequest("SourceUrl is required");
        }

        if (!await _context.Stories.AnyAsync(s => s.Id == storyItem.StoryId))
        {
            return BadRequest("Story with specified StoryId does not exist");
        }

        storyItem.UpdatedAt = DateTime.UtcNow;
        _context.Entry(storyItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StoryItemExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет элемент истории
    /// </summary>
    /// <param name="id">Идентификатор элемента истории</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoryItem(Guid id)
    {
        var storyItem = await _context.StoryItems.FindAsync(id);
        if (storyItem == null)
        {
            return NotFound();
        }

        _context.StoryItems.Remove(storyItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StoryItemExists(Guid id)
    {
        return _context.StoryItems.Any(e => e.Id == id);
    }
} 