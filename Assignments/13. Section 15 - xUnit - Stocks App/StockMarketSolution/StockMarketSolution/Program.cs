using Service;
using StockApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
string? finnhubToken = builder.Configuration["FinnhubToken"];
builder.Services.AddScoped<FinnhubCompanyProfileService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new FinnhubCompanyProfileService(httpClientFactory, configuration, finnhubToken);
}); builder.Services.AddScoped<FinnhubStockPriceQuoteService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new FinnhubStockPriceQuoteService(httpClientFactory, configuration, finnhubToken);
});

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection(nameof(TradingOptions)));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
