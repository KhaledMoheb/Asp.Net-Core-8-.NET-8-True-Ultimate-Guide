// Create a new WebApplication instance
using Entity.DbContexts;
using Rotativa.AspNetCore;
using Service;
using ServiceContract;
using StockMarketSolution.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {

    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
    .ReadFrom.Services(services)
    .WriteTo.Console(); //read out current app's services and make them available to serilog
});

// Add MVC controllers and views support
builder.Services.AddControllersWithViews();

// Add HttpClient for making HTTP requests
builder.Services.AddHttpClient();

// Retrieve the Finnhub API token from configuration
string? finnhubToken = builder.Configuration["FinnhubToken"];

// Register FinnhubService as a scoped service
builder.Services.AddScoped<IFinnhubService, FinnhubService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new FinnhubService(httpClientFactory, configuration, finnhubToken);
});

// Register StocksService as a singleton service
builder.Services.AddScoped<IStocksService, StocksService>();

// Configure TradingOptions from configuration
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection(nameof(TradingOptions)));

builder.Services.AddDbContext<StockMarketDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Build the application
var app = builder.Build();

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