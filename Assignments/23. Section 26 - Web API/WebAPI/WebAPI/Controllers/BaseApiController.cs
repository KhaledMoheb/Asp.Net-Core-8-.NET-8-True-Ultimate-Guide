using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Web
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly ILogger<BaseApiController> _logger;

        protected BaseApiController(ILogger<BaseApiController> logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleException(Exception ex)
        {
            _logger.LogError(ex, "An error occurred");
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            };
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}
