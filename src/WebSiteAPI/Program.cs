using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace WebSiteAPI;

/// <summary>
/// Стартовая точка приложения
/// </summary>
public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(x => x.UseStartup<Startup>().ConfigureKestrel(options =>
            {
                _ = int.TryParse(Environment.GetEnvironmentVariable("HTTP_PORT")!, out var httpPort);

                options.Listen(
                    IPAddress.Any,
                    httpPort,
                    listenOptions => listenOptions.Protocols = HttpProtocols.Http1);
            }));
    }
}