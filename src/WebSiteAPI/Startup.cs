using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebSiteAPI.DataAccess;

namespace WebSiteAPI;

/// <summary>
/// 
/// </summary>
public class Startup
{
    private IConfiguration Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        
        var connectionString = Configuration.GetConnectionString("WebSiteDb");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        // Настройка Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Store API",
                Version = "v1",
                Description = "API для управления магазином",
                Contact = new OpenApiContact
                {
                    Name = "Ваше имя",
                    Email = "ваш-email@example.com"
                }
            });

            // Включение XML комментариев для документации
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        // Включение Swagger UI
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store API v1");
            c.RoutePrefix = string.Empty; // Открыть Swagger по умолчанию при запуске приложения
        });
        
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            // if (context != null)
            // {
            //     DbInitializer.InitializeStartValuesIfEmpty(context); // Инициализация начальных данных
            // }
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}