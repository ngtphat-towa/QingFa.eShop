using ErrorOr;

namespace QingFa.EShop.Domain.DomainModels.Errors
{
    /// <summary>
    /// Defines a set of error codes used across the application.
    /// </summary>
    public static class ErrorCodes
    {
        public const string NullArgument = "NULL_ARGUMENT";
        public const string InvalidArgument = "INVALID_ARGUMENT";
        public const string NotFound = "NOT_FOUND";
        public const string OperationFailed = "OPERATION_FAILED";
        public const string UnauthorizedAccess = "UNAUTHORIZED_ACCESS";
        public const string Conflict = "CONFLICT";
        public const string AuthenticationFailed = "AUTHENTICATION_FAILED";
        public const string Forbidden = "FORBIDDEN";
        public const string ValidationError = "VALIDATION_ERROR";
        public const string RateLimited = "RATE_LIMITED";
        public const string Timeout = "TIMEOUT";
    }

    /// <summary>
    /// Provides static methods to create common error types.
    /// </summary>
    public static class CoreErrors
    {
        public static Error NullArgument(string argName) =>
            Error.Validation(ErrorCodes.NullArgument, $"{argName} cannot be null.");

        public static Error InvalidArgument(string argName) =>
            Error.Validation(ErrorCodes.InvalidArgument, $"{argName} is invalid.");

        public static Error NotFound(string entityName) =>
            Error.NotFound(ErrorCodes.NotFound, $"{entityName} was not found.");

        public static Error OperationFailed(string operation) =>
            Error.Failure(ErrorCodes.OperationFailed, $"Operation failed: {operation}");

        public static Error UnauthorizedAccess(string action) =>
            Error.Failure(ErrorCodes.UnauthorizedAccess, $"Unauthorized access for action: {action}");

        public static Error Conflict(string entity) =>
            Error.Failure(ErrorCodes.Conflict, $"{entity} already exists or is in conflict.");

        public static Error AuthenticationFailed(string user) =>
            Error.Failure(ErrorCodes.AuthenticationFailed, $"Authentication failed for user: {user}");

        public static Error Forbidden(string resource) =>
            Error.Failure(ErrorCodes.Forbidden, $"Access to resource {resource} is forbidden.");

        public static Error ValidationError(string fieldName, string message) =>
            Error.Validation(ErrorCodes.ValidationError, $"{fieldName}: {message}");

        public static Error RateLimited() =>
            Error.Failure(ErrorCodes.RateLimited, "Rate limit exceeded. Please try again later.");

        public static Error Timeout(string operation) =>
            Error.Failure(ErrorCodes.Timeout, $"Operation timed out: {operation}");
    }
}
