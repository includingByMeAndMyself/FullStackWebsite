using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Store.API.Models;

namespace Store.API.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Автоматически находит и применяет все классы, реализующие интерфейс IEntityTypeConfiguration<T> в текущей сборке.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}