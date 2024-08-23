using System.Net;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

internal static class ErrorHandlingMiddlewareHelpers
{

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

    public static object? FormatStackTrace(string stackTrace)
    {
        if (string.IsNullOrEmpty(stackTrace))
        {
            return null;
        }

        // Split the stack trace into lines
        var lines = stackTrace
            .Replace("\r\n", "\n") // Normalize line endings
            .Split('\n');

        // Parse each line into a structured object
        var stackTraceFrames = lines
            .Select(line => new { Frame = line })
            .ToList();

        return stackTraceFrames;
    }

    //public static object? FormatStackTrace(string? stackTrace)
    //{
    //    return string.IsNullOrEmpty(stackTrace) ? string.Empty : stackTrace;
    //}


    public static Task WriteResponseAsync(HttpContext context, ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            var jsonResponse = JsonSerializer.Serialize(problemDetails, JsonSerializerOptions);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(jsonResponse);
        }

}