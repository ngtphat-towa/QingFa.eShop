using System.Net;
using System.Text.Json;

using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = HttpStatusCode.InternalServerError;
                var message = "An unexpected error occurred.";
                string? details = null;

                if (ex is CoreException coreException)
                {
                    statusCode = (HttpStatusCode)coreException.StatusCode;
                    message = coreException.Message;
                    details = coreException.Details;
                }
                else
                {
                    _logger.LogError(ex, "An unhandled exception occurred while processing the request for path {RequestPath}", context.Request.Path);
                }

                var response = new
                {
                    StatusCode = (int)statusCode,
                    Message = message,
                    Details = details
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
