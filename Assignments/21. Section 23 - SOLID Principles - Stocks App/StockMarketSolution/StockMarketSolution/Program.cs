using Services.DbContexts;
using Rotativa.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StockMarketSolution.Middleware;
using Repositories;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using ServiceContracts.StocksService;
using Services.FinnhubService;
using Services.StocksService;
using Entity.Model;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration) // Read configuration settings from built-in IConfiguration
    .ReadFrom.Services(services)
    .WriteTo.Console(); // Write out current app's services and make them available to Serilog
});

// Add MVC controllers and views support
builder.Services.AddControllersWithViews();

// Add HttpClient for making HTTP requests
builder.Services.AddHttpClient();

builder.Services.AddTransient<IBuyOrdersService, StocksBuyOrdersService>();
builder.Services.AddTransient<ISellOrdersService, StocksSellOrdersService>();

builder.Services.AddTransient<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
builder.Services.AddTransient<IFinnhubStockPriceQuoteService, FinnhubStockPriceQuoteService>();
builder.Services.AddTransient<IFinnhubStocksService, FinnhubStocksService>();
builder.Services.AddTransient<IFinnhubSearchStocksService, FinnhubSearchStocksService>();

builder.Services.AddTransient<IStocksRepository, StocksRepository>();

// Retrieve the Finnhub API token from configuration
string? finnhubToken = builder.Configuration["FinnhubToken"];

// Register FinnhubService as a scoped service
builder.Services.AddTransient<IFinnhubRepository, FinnhubRepository>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new FinnhubRepository(httpClientFactory, configuration, finnhubToken);
});

// Configure TradingOptions from configuration
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection(nameof(TradingOptions)));

builder.Services.AddDbContext<StockMarketDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add ExceptionHandlingMiddleware as a service
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Build the application
var app = builder.Build();

app.UseSerilogRequestLogging();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Use custom exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enable serving static files (like CSS, JavaScript) from wwwroot folder
app.UseStaticFiles();

// Enable routing middleware to handle incoming requests
app.UseRouting();

// Map controllers as endpoints for handling HTTP requests
app.MapControllers();

RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

// Start the application and begin listening for incoming HTTP requests
app.Run();

public partial class Program { }
