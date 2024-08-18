namespace QingFa.EShop.Domain.Core.Exceptions
{
    /// <summary>
    /// Represents a core exception in the domain model, used for handling various error scenarios.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CoreException"/> class with a specified error message, status code, error code, and additional details.
    /// </remarks>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="statusCode">The HTTP status code associated with this exception.</param>
    /// <param name="errorCode">The application-specific error code associated with this exception.</param>
    /// <param name="details">Additional details about the error.</param>
    /// <param name="innerException">The inner exception to be wrapped.</param>
    public class CoreException(string message, int statusCode = 500, string? errorCode = null, string? details = null, Exception? innerException = null) : Exception(message, innerException)
    {
        /// <summary>
        /// Gets the HTTP status code associated with this exception.
        /// </summary>
        public int StatusCode { get; } = statusCode;

        /// <summary>
        /// Gets the application-specific error code associated with this exception.
        /// </summary>
        public string? ErrorCode { get; } = errorCode;

        /// <summary>
        /// Gets additional details about the error.
        /// </summary>
        public string? Details { get; } = details;

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with the specified message and status code.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException Create(string message, int statusCode = 500, string? errorCode = null, string? details = null, Exception? innerException = null)
        {
            return new CoreException(message, statusCode, errorCode, details, innerException);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating a null argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is null.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NullArgument(string argumentName, int statusCode = 400, string? errorCode = "NULL_ARGUMENT", string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
            {
                throw new ArgumentException("Argument name cannot be null or empty.", nameof(argumentName));
            }

            return new CoreException($"Argument '{argumentName}' cannot be null.", statusCode, errorCode, details, innerException);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating a null or empty argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is null or empty.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NullOrEmptyArgument(string argumentName, int statusCode = 400, string? errorCode = "NULL_OR_EMPTY_ARGUMENT", string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
            {
                throw new ArgumentException("Argument name cannot be null or empty.", nameof(argumentName));
            }

            return new CoreException($"Argument '{argumentName}' cannot be null or empty.", statusCode, errorCode, details, innerException);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating an invalid argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is invalid.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException InvalidArgument(string argumentName, int statusCode = 400, string? errorCode = "INVALID_ARGUMENT", string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
            {
                throw new ArgumentException("Argument name cannot be null or empty.", nameof(argumentName));
            }

            return new CoreException($"Argument '{argumentName}' is invalid.", statusCode, errorCode, details, innerException);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating that an entity was not found.
        /// </summary>
        /// <param name="entityName">The name of the entity that was not found.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NotFound(string entityName, int statusCode = 404, string? errorCode = "ENTITY_NOT_FOUND", string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                throw new ArgumentException("Entity name cannot be null or empty.", nameof(entityName));
            }

            return new CoreException($"Entity '{entityName}' was not found.", statusCode, errorCode, details, innerException);
        }
    }

    /// <summary>
    /// Represents an exception that is thrown when a user does not have the required permissions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PermissionException"/> class with a specified error message and details.
    /// </remarks>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="details">Additional details about the error.</param>
    /// <param name="innerException">The inner exception to be wrapped.</param>
    public class PermissionException(string message, string? details = null, Exception? innerException = null) : CoreException(message, 403, "PERMISSION_DENIED", details, innerException)
    {

        /// <summary>
        /// Creates a new instance of <see cref="PermissionException"/> with the specified message indicating insufficient permissions.
        /// </summary>
        /// <param name="resource">The resource for which permission was denied.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="PermissionException"/> instance.</returns>
        public static PermissionException Denied(string resource, string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentException("Resource name cannot be null or empty.", nameof(resource));
            }

            return new PermissionException($"Access to the resource '{resource}' is denied.", details, innerException);
        }
    }
}

