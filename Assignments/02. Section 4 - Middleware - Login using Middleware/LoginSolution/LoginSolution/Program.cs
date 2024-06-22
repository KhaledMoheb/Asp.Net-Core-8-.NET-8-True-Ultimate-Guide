using LoginSolution.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseValidateLoginMiddleware();

app.Run();
