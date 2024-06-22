var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    var queryCollection = context.Request.Query;

    // Check if 'firstNumber' query parameter is present
    if (!queryCollection.ContainsKey("firstNumber"))
    {
        context.Response.StatusCode = 400; // Bad Request
        await context.Response.WriteAsync("Invalid input for 'firstNumber'.");
        return;
    }

    // Check if 'secondNumber' query parameter is present
    if (!queryCollection.ContainsKey("secondNumber"))
    {
        context.Response.StatusCode = 400; // Bad Request
        await context.Response.WriteAsync("Invalid input for 'secondNumber'.");
        return;
    }

    // Check if 'operation' query parameter is present
    if (!queryCollection.ContainsKey("operation"))
    {
        context.Response.StatusCode = 400; // Bad Request
        await context.Response.WriteAsync("Invalid input for 'operation'.");
        return;
    }

    // Try to parse the 'firstNumber' query parameter to a double
    if (!double.TryParse(queryCollection["firstNumber"], out double firstNumber))
    {
        context.Response.StatusCode = 400; // Bad Request
        await context.Response.WriteAsync("Invalid input for 'firstNumber'.");
        return;
    }

    // Try to parse the 'secondNumber' query parameter to a double
    if (!double.TryParse(queryCollection["secondNumber"], out double secondNumber))
    {
        context.Response.StatusCode = 400; // Bad Request
        await context.Response.WriteAsync("Invalid input for 'secondNumber'.");
        return;
    }

    // Retrieve the 'operation' query parameter as a string
    string operation = queryCollection["operation"].ToString();
    // Handle the arithmetic operation based on the value of 'operation'
    switch (operation)
    {
        default:
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Invalid input for 'operation'");
            break;

        case "add":
            context.Response.StatusCode = 200; // OK
            await context.Response.WriteAsync((firstNumber + secondNumber).ToString());
            break;

        case "subtract":
            context.Response.StatusCode = 200; // OK
            await context.Response.WriteAsync((firstNumber - secondNumber).ToString());
            break;

        case "multiply":
            context.Response.StatusCode = 200; // OK
            await context.Response.WriteAsync((firstNumber * secondNumber).ToString());
            break;

        case "divide":
            if (secondNumber == 0)
            {
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("Cannot divide by 0");
            }
            else
            {
                context.Response.StatusCode = 200; // OK
                await context.Response.WriteAsync((firstNumber / secondNumber).ToString());
            }
            break;

        case "modulus":
            context.Response.StatusCode = 200; // OK
            await context.Response.WriteAsync((firstNumber % secondNumber).ToString());
            break;
    }
});

app.Run();