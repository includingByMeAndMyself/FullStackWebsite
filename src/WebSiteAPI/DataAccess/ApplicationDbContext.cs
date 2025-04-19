using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebSiteAPI.Models;

namespace WebSiteAPI.DataAccess;

/// <inheritdoc />
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Product> Products { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<ProductItem> ProductItems { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Cart> Carts { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<CartItem> CartItems { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Ingredient> Ingredients { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Order> Orders { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<Story> Stories { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<StoryItem> StoryItems { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<VerificationCode> VerificationCodes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Автоматически находит и применяет все классы, реализующие интерфейс IEntityTypeConfiguration<T> в текущей сборке.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}