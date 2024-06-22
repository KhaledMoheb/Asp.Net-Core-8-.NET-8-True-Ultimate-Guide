using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Registering services for dependency injection
builder.Services.AddScoped<ICitiesService, CitiesService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Serve static files
app.UseStaticFiles();

// Use routing middleware
app.UseRouting();

// Map controller endpoints
app.MapControllers();

app.Run();
