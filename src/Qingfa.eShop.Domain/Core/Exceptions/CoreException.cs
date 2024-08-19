namespace QingFa.EShop.Domain.Core.Exceptions
{
    /// <summary>
    /// Represents a core exception in the domain model, used for handling various error scenarios.
    /// </summary>
    public class CoreException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code associated with this exception.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Gets the application-specific error code associated with this exception.
        /// </summary>
        public string? ErrorCode { get; }

        /// <summary>
        /// Gets additional details about the error.
        /// </summary>
        public string? Details { get; }

        /// <summary>
        /// Gets a short, human-readable summary of the exception.
        /// </summary>
        public string? Title { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException"/> class with a specified error message, status code, error code, title, and additional details.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The HTTP status code associated with this exception.</param>
        /// <param name="errorCode">The application-specific error code associated with this exception.</param>
        /// <param name="title">A short, human-readable summary of the exception.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        public CoreException(string message, int statusCode = 500, string? errorCode = null, string? title = null, string? details = null, Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Title = title;
            Details = details;
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with the specified message, status code, error code, title, and additional details.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="title">A short, human-readable summary of the exception.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException Create(string message, int statusCode = 500, string? errorCode = null, string? title = null, string? details = null, Exception? innerException = null)
        {
            return new CoreException(message, statusCode, errorCode, title, details, innerException);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating a null argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is null.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="title">A short, human-readable summary of the exception.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NullArgument(string argumentName, int statusCode = 400, string? errorCode = "NULL_ARGUMENT", string? title = "Null Argument", string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
            {
                throw new ArgumentException("Argument name cannot be null or empty.", nameof(argumentName));
            }

            return new CoreException($"Argument '{argumentName}' cannot be null.", statusCode, errorCode, title, details, innerException);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating a null or empty argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is null or empty.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="title">A short, human-readable summary of the exception.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NullOrEmptyArgument(string argumentName, int statusCode = 400, string? errorCode = "NULL_OR_EMPTY_ARGUMENT", string? title = "Null or Empty Argument", string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
            {
                throw new ArgumentException("Argument name cannot be null or empty.", nameof(argumentName));
            }

            return new CoreException($"Argument '{argumentName}' cannot be null or empty.", statusCode, errorCode, title, details, innerException);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating an invalid argument.
        /// </summary>
        /// <param name="argumentName">The name of the argument that is invalid.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="title">A short, human-readable summary of the exception.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException InvalidArgument(string argumentName, int statusCode = 400, string? errorCode = "INVALID_ARGUMENT", string? title = "Invalid Argument", string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
            {
                throw new ArgumentException("Argument name cannot be null or empty.", nameof(argumentName));
            }

            return new CoreException($"Argument '{argumentName}' is invalid.", statusCode, errorCode, title, details, innerException);
        }

        /// <summary>
        /// Creates a new instance of <see cref="CoreException"/> with a message indicating that an entity was not found.
        /// </summary>
        /// <param name="entityName">The name of the entity that was not found.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorCode">The application-specific error code.</param>
        /// <param name="title">A short, human-readable summary of the exception.</param>
        /// <param name="details">Additional details about the error.</param>
        /// <param name="innerException">The inner exception to be wrapped.</param>
        /// <returns>A new <see cref="CoreException"/> instance.</returns>
        public static CoreException NotFound(string entityName, int statusCode = 404, string? errorCode = "ENTITY_NOT_FOUND", string? title = "Entity Not Found", string? details = null, Exception? innerException = null)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                throw new ArgumentException("Entity name cannot be null or empty.", nameof(entityName));
            }

            return new CoreException($"Entity '{entityName}' was not found.", statusCode, errorCode, title, details, innerException);
        }
    }
}
