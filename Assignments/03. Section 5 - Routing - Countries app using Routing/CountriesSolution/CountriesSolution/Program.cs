var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // Endpoint for countries with IDs from 1 to 5
    endpoints.MapGet("/country/{id:int:range(1, 5)}", async context =>
    {
        // Dictionary of countries
        Dictionary<int, string> countries = new Dictionary<int, string>
        {
            { 1, "United States" },
            { 2, "Canada" },
            { 3, "United Kingdom" },
            { 4, "India" },
            { 5, "Japan" },
        };

        // Extract the 'id' parameter from the route values
        int id = Convert.ToInt32(context.Request.RouteValues["id"]);

        // Set HTTP status code to 200 (OK)
        context.Response.StatusCode = 200;

        // Write the country name corresponding to the 'id'
        await context.Response.WriteAsync(countries[id]);
    });

    // Endpoint for countries with IDs from 6 to 100
    endpoints.Map("/country/{id:int:range(6, 100)}", async context =>
    {
        // Set HTTP status code to 404 (Not Found)
        context.Response.StatusCode = 404;

        // Write a message indicating no country found
        await context.Response.WriteAsync("[No Country]");
    });

    // Endpoint for countries with IDs greater than 100
    endpoints.MapGet("/country/{id:int:range(101, 999999999)}", async context =>
    {
        // Set HTTP status code to 400 (Bad Request)
        context.Response.StatusCode = 400;

        // Write a message indicating the range of valid IDs
        await context.Response.WriteAsync("The CountryID should be between 1 and 100");
    });
});
app.Run();
