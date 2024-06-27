using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Entity.DbContexts;
using ServiceContract;
using Service;
using StockMarketSolution.Models;

namespace Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json")
                      .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                      .AddUserSecrets<Program>()  // Ensure user secrets are loaded
                      .AddEnvironmentVariables();
            });

            builder.ConfigureServices(services => {
                // Remove existing DbContext registration
                var descriptor = services.SingleOrDefault(temp => temp.ServiceType == typeof(DbContextOptions<StockMarketDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add in-memory database for testing
                services.AddDbContext<StockMarketDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DatabaseForTesting");
                });

                // Add HttpClient for making HTTP requests
                services.AddHttpClient();

                // Register FinnhubService as a scoped service
                services.AddScoped<IFinnhubService, FinnhubService>(provider =>
                {
                    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var finnhubToken = configuration["FinnhubToken"];
                    return new FinnhubService(httpClientFactory, configuration, finnhubToken);
                });

                // Register StocksService as a scoped service
                services.AddScoped<IStocksService, StocksService>();

                // Configure TradingOptions from configuration
                services.Configure<TradingOptions>(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection(nameof(TradingOptions)));
            });
        }
    }
}
