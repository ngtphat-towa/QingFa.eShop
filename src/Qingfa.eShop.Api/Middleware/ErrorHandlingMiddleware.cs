using System.Net;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly bool _isDevelopment;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _isDevelopment = env.IsDevelopment();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (CoreException ex)
            {
                await HandleCoreExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Validation Error",
                Detail = _isDevelopment ? ex.Message : "One or more validation errors occurred.",
                Instance = context.Request.Path
            };

            if (_isDevelopment)
            {
                var validationErrors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToList()
                    );

                problemDetails.Extensions["errors"] = validationErrors;
            }

            await ErrorHandlingMiddlewareHelpers.WriteResponseAsync(context, problemDetails, HttpStatusCode.BadRequest);
        }

        private async Task HandleCoreExceptionAsync(HttpContext context, CoreException ex)
        {
            var problemDetails = new ProblemDetails
            {
                Status = ex.StatusCode,
                Title = ex.Title ?? "Application Error",
                Detail = _isDevelopment ? ex.Message : "An application error occurred.",
                Instance = context.Request.Path
            };

            if (_isDevelopment && !string.IsNullOrEmpty(ex.Details))
            {
                problemDetails.Extensions["details"] = ErrorHandlingMiddlewareHelpers.FormatStackTrace(ex.Details);
            }

            await ErrorHandlingMiddlewareHelpers.WriteResponseAsync(context, problemDetails, (HttpStatusCode)ex.StatusCode);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred while processing the request for path {RequestPath}", context.Request.Path);

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Error",
                Detail = _isDevelopment ? ex.Message : "An unexpected error occurred.",
                Instance = context.Request.Path
            };

            if (_isDevelopment)
            {
                problemDetails.Extensions["stackTrace"] = ErrorHandlingMiddlewareHelpers.FormatStackTrace(ex.StackTrace ?? "An unexpected error occurred.");
            }

            await ErrorHandlingMiddlewareHelpers.WriteResponseAsync(context, problemDetails, HttpStatusCode.InternalServerError);
        }
    }
}
