namespace WebSiteAPI.DataAccess;

/// <summary>
/// 
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public static void InitializeStartValuesIfEmpty(ApplicationDbContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        // context.Products.AddRange(
        //     new Product { Name = "Laptop", Price = 1200m, Stock = 10 },
        //     new Product { Name = "Smartphone", Price = 800m, Stock = 20 }
        // );

        context.SaveChanges();
    }
}
