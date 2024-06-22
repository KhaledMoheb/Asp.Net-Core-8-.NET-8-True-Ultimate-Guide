using Microsoft.Extensions.Primitives;

namespace LoginSolution.Middlewares
{
    public class ValidateLoginMiddleware
    {
        private readonly RequestDelegate _next;
        const string requiredEmail = "admin@example.com";
        const string requiredPassword = "admin1234";

        public ValidateLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == HttpMethods.Post && httpContext.Request.Path == "/")
            {
                using var reader = new StreamReader(httpContext.Request.Body);
                var body = await reader.ReadToEndAsync();

                Dictionary<string, StringValues> parsedQuery = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

                string inputEmail = "", inputPassword = "";
                bool isInvalidEmailOrPassword = false;

                if (!parsedQuery.ContainsKey("email"))
                {
                    isInvalidEmailOrPassword = true;
                    await httpContext.Response.WriteAsync("Invalid input for 'email'");
                    return;
                }

                if (!parsedQuery.ContainsKey("password"))
                {
                    isInvalidEmailOrPassword = true;
                    await httpContext.Response.WriteAsync("Invalid input for 'password'");
                    return;
                }

                if (isInvalidEmailOrPassword)
                {
                    httpContext.Response.StatusCode = 400;
                }
                else
                {
                    inputEmail = parsedQuery["email"].ToString();
                    inputPassword = parsedQuery["password"].ToString();

                    if (inputEmail.Equals(requiredEmail) && inputPassword.Equals(requiredPassword))
                    {
                        await httpContext.Response.WriteAsync("Successful login");
                        httpContext.Response.StatusCode = 200;
                    }
                    else
                    {
                        await httpContext.Response.WriteAsync("Invalid login");
                        httpContext.Response.StatusCode = 401;
                    }
                }
            }

            await _next(httpContext);
        }
    }

    public static class ValidateLoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidateLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateLoginMiddleware>();
        }
    }
}
