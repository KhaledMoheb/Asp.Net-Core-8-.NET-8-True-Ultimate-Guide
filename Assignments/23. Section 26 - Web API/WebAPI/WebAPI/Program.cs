using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.OrderItems;
using WebAPI.Core.ServiceContracts.Orders;
using WebAPI.Core.Services.OrderItems;
using WebAPI.Core.Services.Orders;
using WebAPI.Infrastructure.DBContexts;
using WebAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders Web API", Version = "v1" });
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add repositories and services
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
builder.Services.AddScoped<IOrdersAdderService, OrdersAdderService>();
builder.Services.AddScoped<IOrdersDeleterService, OrdersDeleterService>();
builder.Services.AddScoped<IOrdersFilterService, OrdersFilterService>();
builder.Services.AddScoped<IOrdersGetterService, OrdersGetterService>();
builder.Services.AddScoped<IOrdersUpdaterService, OrdersUpdaterService>();
builder.Services.AddScoped<IOrderItemsAdderService, OrderItemsAdderService>();
builder.Services.AddScoped<IOrderItemsDeleterService, OrderItemsDeleterService>();
builder.Services.AddScoped<IOrderItemsGetterService, OrderItemsGetterService>();
builder.Services.AddScoped<IOrderItemsUpdaterService, OrderItemsUpdaterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders Web API v1");
        c.RoutePrefix = "api-docs"; // Optional: serve Swagger UI at "/api-docs"
    });
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error"); // Custom error handling endpoint
    app.UseHsts(); // Enable HTTPS Strict Transport Security (HSTS) in non-development environments
}

app.UseRouting();
app.MapControllers();

// Run the application
app.Run();
