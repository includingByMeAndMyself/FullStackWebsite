using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.DataAccess;
using WebSiteAPI.Models;

namespace WebSiteAPI.Controllers;

/// <summary>
/// Контроллер для управления историями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр класса StoryController
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    public StoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает список всех историй
    /// </summary>
    /// <returns>Список историй с информацией о пользователе и элементах истории</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Story>>> GetStories()
    {
        return await _context.Stories
            .Include(s => s.UserId)
            .Include(s => s.StoryItems)
            .ToListAsync();
    }

    /// <summary>
    /// Получает историю по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор истории</param>
    /// <returns>История с информацией о пользователе и элементах истории</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Story>> GetStory(Guid id)
    {
        var story = await _context.Stories
            .Include(s => s.UserId)
            .Include(s => s.StoryItems)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (story == null)
        {
            return NotFound();
        }

        return story;
    }

    /// <summary>
    /// Создает новую историю
    /// </summary>
    /// <param name="story">Данные новой истории</param>
    /// <returns>Созданная история</returns>
    [HttpPost]
    public async Task<ActionResult<Story>> CreateStory(Story story)
    {
        if (string.IsNullOrEmpty(story.PreviewImageUrl))
        {
            return BadRequest("PreviewImageUrl is required");
        }

        story.Id = Guid.NewGuid();
        story.CreatedAt = DateTime.UtcNow;
        story.UpdatedAt = DateTime.UtcNow;

        _context.Stories.Add(story);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStory), new { id = story.Id }, story);
    }

    /// <summary>
    /// Обновляет существующую историю
    /// </summary>
    /// <param name="id">Идентификатор истории</param>
    /// <param name="story">Обновленные данные истории</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStory(Guid id, Story story)
    {
        if (id != story.Id)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(story.PreviewImageUrl))
        {
            return BadRequest("PreviewImageUrl is required");
        }

        story.UpdatedAt = DateTime.UtcNow;
        _context.Entry(story).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StoryExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет историю
    /// </summary>
    /// <param name="id">Идентификатор истории</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStory(Guid id)
    {
        var story = await _context.Stories.FindAsync(id);
        if (story == null)
        {
            return NotFound();
        }

        _context.Stories.Remove(story);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StoryExists(Guid id)
    {
        return _context.Stories.Any(e => e.Id == id);
    }
} 