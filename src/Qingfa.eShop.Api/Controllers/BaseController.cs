using System.Net;

using Microsoft.AspNetCore.Mvc;

using QingFa.EShop.Application.Core.Models;

namespace QingFa.EShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.Succeeded)
            {
                return Ok(result.Value);
            }

            return HandleResult(result as Result); // Delegate to existing HandleResult
        }

        protected IActionResult HandleResult(Result result)
        {
            if (result.ErrorCode.HasValue)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = result.ErrorCode.GetValueOrDefault(StatusCodes.Status500InternalServerError),
                    Title = result.ErrorMessage ?? "An error occurred.",
                    Detail = string.Join(", ", result.Errors),
                    Instance = HttpContext.Request.Path
                };

                return result.ErrorCode.Value switch
                {
                    404 => NotFound(problemDetails),
                    400 => BadRequest(problemDetails),
                    401 => Unauthorized(problemDetails),
                    409 => Conflict(problemDetails),
                    422 => UnprocessableEntity(problemDetails),
                    423 => StatusCode(StatusCodes.Status423Locked, problemDetails),
                    412 => StatusCode(StatusCodes.Status412PreconditionFailed, problemDetails),
                    429 => StatusCode(StatusCodes.Status429TooManyRequests, problemDetails),
                    500 => StatusCode(StatusCodes.Status500InternalServerError, problemDetails),
                    _ => StatusCode(result.ErrorCode.GetValueOrDefault(StatusCodes.Status500InternalServerError), problemDetails)
                };
            }

            // Fallback for unexpected errors
            var fallbackProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Instance = HttpContext.Request.Path
            };

            return StatusCode(StatusCodes.Status500InternalServerError, fallbackProblemDetails);
        }

        protected IActionResult HandleException(Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Instance = HttpContext.Request.Path
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}
