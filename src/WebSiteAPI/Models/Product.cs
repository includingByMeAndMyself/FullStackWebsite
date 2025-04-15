
namespace Store.API.Models;

/// <summary>
/// Продукт магазина
/// </summary>
public class Product
{
    /// <summary>
    /// Id продукта
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Наименование продукта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена продукта
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Количество продуктов
    /// </summary>
    public int Stock { get; set; }
    
    //Переделать под объект
    public int Locations { get; set; }   
}
