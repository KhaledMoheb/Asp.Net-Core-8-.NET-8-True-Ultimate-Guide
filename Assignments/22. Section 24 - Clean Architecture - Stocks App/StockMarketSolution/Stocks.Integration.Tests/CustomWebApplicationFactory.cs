using Stocks.Core.Entities.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stocks.Core.ServiceContracts.FinnhubService;
using Stocks.Core.ServiceContracts.StocksService;
using Stocks.Core.Services.FinnhubService;
using Services.StocksService;
using Stocks.Web.Middleware;
using Stocks.Infrastructure.DbContexts;
using Stocks.Core.RepositoryContracts;
using Stocks.Infrastructure.Repositories;

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
                      .AddUserSecrets<Program>()
                      .AddEnvironmentVariables();
            });

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(temp => temp.ServiceType == typeof(DbContextOptions<StockMarketDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<StockMarketDbContext>(options =>
                {
                    options.UseInMemoryDatabase("DatabaseForTesting");
                });

                services.AddHttpClient();

                services.AddTransient<IBuyOrdersService, StocksBuyOrdersService>();
                services.AddTransient<ISellOrdersService, StocksSellOrdersService>();

                services.AddTransient<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
                services.AddTransient<IFinnhubStockPriceQuoteService, FinnhubStockPriceQuoteService>();
                services.AddTransient<IFinnhubStocksService, FinnhubStocksService>();
                services.AddTransient<IFinnhubSearchStocksService, FinnhubSearchStocksService>();

                services.AddTransient<IFinnhubRepository, FinnhubRepository>(provider =>
                {
                    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var finnhubToken = configuration["FinnhubToken"];
                    return new FinnhubRepository(httpClientFactory, configuration, finnhubToken);
                });

                services.Configure<TradingOptions>(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection(nameof(TradingOptions)));

                services.AddTransient<ExceptionHandlingMiddleware>();
            });

            builder.Configure(app =>
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            });
        }
    }
}
